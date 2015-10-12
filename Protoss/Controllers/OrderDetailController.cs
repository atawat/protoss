using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Protoss.Common;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.OrderDetail;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
    [AllowAnonymous]
    [EnableCors("*", "*", "*", SupportsCredentials = true)]
	public class OrderDetailController : ApiController
	{
		private readonly IOrderDetailService _OrderDetailService;

		public OrderDetailController(IOrderDetailService OrderDetailService)
		{
			_OrderDetailService = OrderDetailService;
		}

		public OrderDetailModel Get(int id)
		{
			var entity =_OrderDetailService.GetOrderDetailById(id);
		    var model = new OrderDetailModel
		    {

		        Id = entity.Id,

//		        Product = entity.Product,

		        Count = entity.Count,

                Remark=entity.Remark,

		        TotalPrice = entity.TotalPrice,

//		        Order = entity.Order,

		    };
			return model;
		}
        [HttpGet]
        public HttpResponseMessage Get([FromUri]OrderDetailSearchCondition condition)
		{
			var model = _OrderDetailService.GetOrderDetailsByCondition(condition).Select(c=>new OrderDetailModel
			{

				Id = c.Id,
				ProductName = c.Product.Name,
                Image = c.Product.Image,
				Count = c.Count,
				TotalPrice = c.TotalPrice,
                Remark = c.Remark,
                AddTime = c.Order.Addtime
//				Order = c.Order,

			}).ToList();
            return PageHelper.toJson(new { List = model });
		}

        public List<OrderDetailModel> GetOrderDetailByOrder(int orderId)
        {
            var model = _OrderDetailService.GetOrderDetailByOrderId(orderId).Select(c => new OrderDetailModel
            {

                Id = c.Id,

                ProductName = c.Product.Name,

                Count = c.Count,

                TotalPrice = c.TotalPrice,

                Remark = c.Remark
            }).ToList();
            return model;
        }

		public bool Post(OrderDetailModel model)
		{
			var entity = new OrderDetailEntity
			{

//				Product = model.Product,

				Count = model.Count,

				TotalPrice = model.TotalPrice,

                Remark = model.Remark,

//				Order = model.Order,

			};
			if(_OrderDetailService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(OrderDetailModel model)
		{
			var entity = _OrderDetailService.GetOrderDetailById(model.Id);
			if(entity == null)
				return false;

//			entity.Product = model.Product;

			entity.Count = model.Count;

			entity.TotalPrice = model.TotalPrice;

            entity.Remark = model.Remark;
//			entity.Order = model.Order;

			if(_OrderDetailService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _OrderDetailService.GetOrderDetailById(id);
			if(entity == null)
				return false;
			if(_OrderDetailService.Delete(entity))
				return true;
			return false;
		}
	}
}