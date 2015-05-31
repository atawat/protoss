using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Protoss.Entity.Model;
using Protoss.Models;
using Protoss.Service.Order;
using Protoss.Service.Product;
using YooPoon.Core.Site;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Controllers
{
    [AllowAnonymous]
    public class OrderController : ApiController
    {
        private readonly IOrderService _OrderService;
        private readonly IWorkContext _workContext;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IWorkContext workContext, IProductService productService)
        {
            _OrderService = orderService;
            _workContext = workContext;
            _productService = productService;
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
                PageCount = page,
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

            }).ToList();
            return model;
        }

        public bool Post(CreateOrderModel model)
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

                Adduser = (UserBase)_workContext.CurrentUser,

                Addtime = DateTime.Now,

                Upduser = (UserBase)_workContext.CurrentUser,

                Updtime = DateTime.Now,

                //				Details = model.Details,

                //				Coupon = model.Coupon,

                Type = model.Type,

                PayType = model.PayType,

                LocationX = model.LocationX,

                LocationY = model.LocationY,

            };
            #region Ã÷Ï¸
            var details = (from detail in model.Details
                           let product = _productService.GetProductById(detail.ProductId)
                           where product != null
                           select new OrderDetailEntity
                           {
                               Count = detail.Count,
                               Product = _productService.GetProductById(detail.ProductId),
                               TotalPrice = detail.Count * product.Price,
                               Order = entity
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