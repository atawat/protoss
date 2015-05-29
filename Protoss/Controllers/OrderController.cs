using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.Order;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class OrderController : ApiController
	{
		private readonly IOrderService _OrderService;

		public OrderController(IOrderService OrderService)
		{
			_OrderService = OrderService;
		}

		public OrderModel Get(int id)
		{
			var entity =_OrderService.GetOrderById(id);
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

		    };
			return model;
		}

		public List<OrderModel> Get(OrderSearchCondition condition)
		{
			var model = _OrderService.GetOrdersByCondition(condition).Select(c=>new OrderModel
			{

				Id = c.Id,

				OrderNum = c.OrderNum,

				TotalPrice = c.TotalPrice,

				TransCost = c.TransCost,

				ProductCost = c.ProductCost,

				Discount  = c.Discount ,

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

		public bool Post(OrderModel model)
		{
			var entity = new OrderEntity
			{

				OrderNum = model.OrderNum,

				TotalPrice = model.TotalPrice,

				TransCost = model.TransCost,

				ProductCost = model.ProductCost,

				Discount  = model.Discount ,

				Status = model.Status,

				DeliveryAddress = model.DeliveryAddress,

				IsPrint = model.IsPrint,

				PhoneNumber = model.PhoneNumber,

				Adduser = model.Adduser,

				Addtime = model.Addtime,

				Upduser = model.Upduser,

				Updtime = model.Updtime,

//				Details = model.Details,

//				Coupon = model.Coupon,

				Type = model.Type,

				PayType = model.PayType,

				LocationX = model.LocationX,

				LocationY = model.LocationY,

			};
			if(_OrderService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(OrderModel model)
		{
			var entity = _OrderService.GetOrderById(model.Id);
			if(entity == null)
				return false;

			entity.OrderNum = model.OrderNum;

			entity.TotalPrice = model.TotalPrice;

			entity.TransCost = model.TransCost;

			entity.ProductCost = model.ProductCost;

			entity.Discount  = model.Discount ;

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

			if(_OrderService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _OrderService.GetOrderById(id);
			if(entity == null)
				return false;
			if(_OrderService.Delete(entity))
				return true;
			return false;
		}
	}
}