using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Models;
using Protoss.Service.Order;
using Protoss.Service.Product;
using YooPoon.Core.Site;
using YooPoon.WebFramework.User.Entity;
using YooPoon.WebFramework.User.Services;
using System.Net.Http;
using Newtonsoft.Json;

namespace Protoss.Controllers
{
    [AllowAnonymous]
    [EnableCors("*", "*", "*", SupportsCredentials = true)]
    public class OrderController : ApiController
    {
        private readonly IOrderService _OrderService;
        private readonly IWorkContext _workContext;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IWorkContext workContext, IProductService productService,IUserService userService)
        {
            _OrderService = orderService;
            _workContext = workContext;
            _productService = productService;
            _userService = userService;
        }

        public OrderModel Get(int id)
        {
            var entity = _OrderService.GetOrderById(id);
            var model = new OrderModel
            {

                Id = entity.Id,

                OrderNum = entity.OrderNum,

                TotalPrice = entity.TotalPrice,

                TransCost = entity.TransCost,

                ProductCost = entity.ProductCost,

                Discount = entity.Discount,

                Status = entity.Status,

                DeliveryAddress = entity.DeliveryAddress,

                IsPrint = entity.IsPrint,

                PhoneNumber = entity.PhoneNumber,

                Adduser = entity.Adduser,

                Addtime = entity.Addtime,

                Upduser = entity.Upduser,

                Updtime = entity.Updtime,

                //		        Details = entity.Details,

                //		        Coupon = entity.Coupon,

                Type = entity.Type,

                PayType = entity.PayType,

                LocationX = entity.LocationX,

                LocationY = entity.LocationY,

                Details = entity.Details.Select(d => new OrderDetailModel()
                {
                    Count = d.Count,
                    Id = d.Id,
                    ProductId = d.Product.Id,
                    ProductName = d.Product.Name,
                    TotalPrice = d.TotalPrice,
                    UnitPrice = d.Product.Price
                }).ToList()

            };
            return model;
        }

        public List<OrderModel> GetByCondition(int? page = 1,
                                                    int? pageCount = 10,
                                                    string ids ="",
                                                    bool isDescending = false,
                                                    string orderNum = "",
                                                    EnumOrderStatus? status = null,
                                                    string deliveryAddress = "",
                                                    bool? isPrint = null,
                                                    string phoneNumber = "",
                                                    EnumOrderType? type = null,
                                                    EnumPayType? payType = null,
                                                    decimal? locationX = null,
                                                    decimal? locationY = null,
                                                    DateTime? addTimeBegin = null,
                                                    DateTime? addTimeEnd = null,
                                                    EnumOrderSearchOrderBy orderBy = EnumOrderSearchOrderBy.OrderById)
        {
            var condition = new OrderSearchCondition
            {
                AddTimeBegin = addTimeBegin,
                AddTimeEnd = addTimeEnd,
                DeliveryAddress = deliveryAddress,
                Ids = string.IsNullOrEmpty(ids)?null:ids.Split(',').Select(int.Parse).ToArray(),
                IsDescending = isDescending,
                IsPrint = isPrint,
                LocationX = locationX,
                LocationY = locationY,
                OrderBy = orderBy,
                OrderNum = orderNum,
                Page = page,
                PageCount = pageCount,
                PayType = payType,
                PhoneNumber = phoneNumber
            };
            var model = _OrderService.GetOrdersByCondition(condition).Select(c => new OrderModel
            {

                Id = c.Id,

                OrderNum = c.OrderNum,

                TotalPrice = c.TotalPrice,

                TransCost = c.TransCost,

                ProductCost = c.ProductCost,

                Discount = c.Discount,

                Status = c.Status,

                DeliveryAddress = c.DeliveryAddress,

                IsPrint = c.IsPrint,

                PhoneNumber = c.PhoneNumber,

                Adduser = c.Adduser,

                Addtime = c.Addtime,

                Upduser = c.Upduser,

                Updtime = c.Updtime,

                //				Details = c.Details,

                //				Coupon = c.Coupon,

                Type = c.Type,

                PayType = c.PayType,

                LocationX = c.LocationX,

                LocationY = c.LocationY,
                Details = c.Details.Select(d => new OrderDetailModel()
                {
                    Count = d.Count,
                    Id = d.Id,
                    ProductId = d.Product.Id,
                    ProductName = d.Product.Name,
                    TotalPrice = d.TotalPrice,
                    UnitPrice = d.Product.Price,
                    Remark=d.Remark
                }).ToList()

            }).ToList();
            return model;
        }

        public string GetTodayOrderNumber()
        {
            OrderSearchCondition OSC = new OrderSearchCondition()
            {
                OrderNum = DateTime.Now.ToString("yyyyMMdd") 
            };
            return "" + _OrderService.GetOrderCount(OSC);
        }

        public string GetTodayNoPrintNumber()
        {
            OrderSearchCondition OSC = new OrderSearchCondition()
            {
                OrderNum = DateTime.Now.ToString("yyyyMMdd"),
                IsPrint=false
            };
            return ""+_OrderService.GetOrderCount(OSC);
        }


        public bool Post([FromBody]CreateOrderModel model)
        {
            var entity = new OrderEntity
            {

                OrderNum = GetNewOrderNum(),

                TransCost = GetTransCost(model.LocationX, model.LocationY),           //Todo:use db data

                Discount = model.Discount,          //Todo:use db data

                Status = EnumOrderStatus.Created,

                DeliveryAddress = model.DeliveryAddress,

                IsPrint = false,

                PhoneNumber = model.PhoneNumber,

                Adduser = model.Type == EnumOrderType.OffLine?_userService.GetUserByName("admin"):(UserBase)_workContext.CurrentUser,

                Addtime = DateTime.Now,

                Upduser = model.Type == EnumOrderType.OffLine ? _userService.GetUserByName("admin") : (UserBase)_workContext.CurrentUser,

                Updtime = DateTime.Now,

                //				Details = model.Details,

                //				Coupon = model.Coupon,

                Type = model.Type,

                PayType = model.PayType,

                LocationX = model.LocationX,

                LocationY = model.LocationY,

            };
            #region 明细
            var details = (from detail in model.Details
                           let product = _productService.GetProductById(detail.ProductId)
                           where product != null
                           select new OrderDetailEntity
                           {
                               Count = detail.Count,
                               Product = _productService.GetProductById(detail.ProductId),
                               TotalPrice = detail.Count * product.Price,
                               Order = entity,
                               Remark = detail.Remark
                           }).ToList();

            entity.ProductCost = details.Sum(d => d.TotalPrice);
            entity.TotalPrice = entity.ProductCost - entity.Discount + entity.TransCost;
            entity.Details = details;

            #endregion
            if (_OrderService.Create(entity).Id > 0)
            {
                return true;
            }
            return false;
        }

        public OrderModel CreateOrder([FromBody]CreateOrderModel model)
        {
            var OrderEntity = new OrderEntity
            {
                TotalPrice=model.TotalPrice,

                OrderNum = GetNewOrderNum(),

                TransCost = GetTransCost(model.LocationX, model.LocationY),           //Todo:use db data

                Discount = model.Discount,          //Todo:use db data

                Status = EnumOrderStatus.Created,

                DeliveryAddress = model.DeliveryAddress,

                IsPrint = false,

                PhoneNumber = model.PhoneNumber,

                Adduser = model.Type == EnumOrderType.OffLine ? _userService.GetUserByName("admin") : (UserBase)_workContext.CurrentUser,

                Addtime = DateTime.Now,

                Upduser = model.Type == EnumOrderType.OffLine ? _userService.GetUserByName("admin") : (UserBase)_workContext.CurrentUser,

                Updtime = DateTime.Now,

                //				Details = model.Details,

                //				Coupon = model.Coupon,

                Type = model.Type,

                PayType = model.PayType,

                LocationX = model.LocationX,

                LocationY = model.LocationY,

            };
            #region 明细
            var details = (from detail in model.Details
                           let product = _productService.GetProductById(detail.ProductId)
                           where product != null
                           select new OrderDetailEntity
                           {
                               Count = detail.Count,
                               Product = _productService.GetProductById(detail.ProductId),
                               TotalPrice = detail.Count * product.Price,
                               Order = OrderEntity,
                               Remark = detail.Remark
                           }).ToList();

            OrderEntity.ProductCost = details.Sum(d => d.TotalPrice);
            OrderEntity.TotalPrice = OrderEntity.ProductCost - OrderEntity.Discount + OrderEntity.TransCost;
            OrderEntity.Details = details;
            #endregion
            var OETemp = _OrderService.Create(OrderEntity);
            var entity = _OrderService.GetOrderById(OETemp.Id);
            var oe = new OrderModel
            {

                Id = entity.Id,

                OrderNum = entity.OrderNum,

                TotalPrice = entity.TotalPrice,

                TransCost = entity.TransCost,

                ProductCost = entity.ProductCost,

                Discount = entity.Discount,

                Status = entity.Status,

                DeliveryAddress = entity.DeliveryAddress,

                IsPrint = entity.IsPrint,

                PhoneNumber = entity.PhoneNumber,

                Adduser = entity.Adduser,

                Addtime = entity.Addtime,

                Upduser = entity.Upduser,

                Updtime = entity.Updtime,

                //		        Details = entity.Details,

                //		        Coupon = entity.Coupon,

                Type = entity.Type,

                PayType = entity.PayType,

                LocationX = entity.LocationX,

                LocationY = entity.LocationY,

                Details = entity.Details.Select(d => new OrderDetailModel()
                {
                    Count = d.Count,
                    Id = d.Id,
                    ProductId = d.Product.Id,
                    ProductName = d.Product.Name,
                    TotalPrice = d.TotalPrice,
                    UnitPrice = d.Product.Price,
                    Remark = d.Remark
                }).ToList()

            };
            return oe;
        }
        /// <summary>
        /// 修改订单状态
		/// 新建的
		/// 已付款
		/// 送货中
		/// 已收货
		/// 已取消
		/// 已完成
        /// <param name="orderId"></param>
        /// <param name="OrderStatus"></param>
        /// <returns></returns>
        public bool updataOrderByOrderId(int orderId, EnumOrderStatus OrderStatus)
        {
            OrderEntity OE=_OrderService.GetOrderById(orderId);
            OE.Status = OrderStatus;
            try
            {
                _OrderService.Update(OE);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
           
        }

        /// <summary>
        /// 修改订单打印状态
        /// <param name="orderId"></param>
        /// <param name="OrderStatus"></param>
        /// <returns></returns>
        public bool GetUpdataOrderIsPrintStatusByOrderId(int orderId, bool isPrint)
        {
            OrderEntity OE = _OrderService.GetOrderById(orderId);
            OE.IsPrint = isPrint;
            try
            {
                _OrderService.Update(OE);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public decimal GetTransCost(decimal locationX, decimal locationY)
        {
            return 0;
        }

        private string GetNewOrderNum()
        {
            var now = DateTime.Today;
            var condition = new OrderSearchCondition
            {
                AddTimeBegin = now,
                AddTimeEnd = now.AddDays(1)
            };
            var count = _OrderService.GetOrderCount(condition);
            return DateTime.Now.ToString("yyyyMMddHHmmss") + count.ToString("000000");
        }

        public bool Put(OrderModel model)
        {
            var entity = _OrderService.GetOrderById(model.Id);
            if (entity == null)
                return false;

            entity.OrderNum = model.OrderNum;

            entity.TotalPrice = model.TotalPrice;

            entity.TransCost = model.TransCost;

            entity.ProductCost = model.ProductCost;

            entity.Discount = model.Discount;

            entity.Status = model.Status;

            entity.DeliveryAddress = model.DeliveryAddress;

            entity.IsPrint = model.IsPrint;

            entity.PhoneNumber = model.PhoneNumber;

            entity.Adduser = model.Adduser;

            entity.Addtime = model.Addtime;

            entity.Upduser = model.Upduser;

            entity.Updtime = model.Updtime;

            //			entity.Details = model.Details;

            //			entity.Coupon = model.Coupon;

            entity.Type = model.Type;

            entity.PayType = model.PayType;

            entity.LocationX = model.LocationX;

            entity.LocationY = model.LocationY;

            if (_OrderService.Update(entity) != null)
                return true;
            return false;
        }

        public bool Delete(int id)
        {
            var entity = _OrderService.GetOrderById(id);
            if (entity == null)
                return false;
            if (_OrderService.Delete(entity))
                return true;
            return false;
        }
    }
}