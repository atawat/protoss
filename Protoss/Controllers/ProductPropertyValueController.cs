//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text.RegularExpressions;
//using System.Web.Http;
//using System.Web.Http.Cors;
//using Protoss.Entity.Model;
//using Protoss.Service.ProductPropertyValue;
//using YooPoon.Core.Site;
//using YooPoon.WebFramework.API;
//using Protoss.Models;
//
//namespace Protoss.Controllers
//{
//	public class ProductPropertyValueController : ApiController
//	{
//		private readonly IProductPropertyValueService _ProductPropertyValueService;
//
//		public ProductPropertyValueController(IProductPropertyValueService ProductPropertyValueService)
//		{
//			_ProductPropertyValueService = ProductPropertyValueService;
//		}
//
//		public ProductPropertyValueModel Get(int id)
//		{
//			var entity =_ProductPropertyValueService.GetProductPropertyValueById(id);
//		    var model = new ProductPropertyValueModel
//		    {
//
//		        Id = entity.Id,
//
//		        PropertyName = entity.Property.PropertyName,
//
//		        PropertyValue = entity.PropertyValue.Value,
//
//		        ProductId = entity.Product.Id,
//
//		        Adduser = entity.Adduser,
//
//		        Addtime = entity.Addtime,
//
//		        UpdUser = entity.UpdUser,
//
//		        UpdTime = entity.UpdTime,
//
//		    };
//			return model;
//		}
//
//		public List<ProductPropertyValueModel> Get(ProductPropertyValueSearchCondition condition)
//		{
//			var model = _ProductPropertyValueService.GetProductPropertyValuesByCondition(condition).Select(c=>new ProductPropertyValueModel
//			{
//
//				Id = c.Id,
//
//				PropertyName = c.Property.PropertyName,
//
//				PropertyValue = c.PropertyValue.Value,
//
//				ProductId = c.Product.Id,
//
//				Adduser = c.Adduser,
//
//				Addtime = c.Addtime,
//
//				UpdUser = c.UpdUser,
//
//				UpdTime = c.UpdTime,
//
//			}).ToList();
//			return model;
//		}
//
////		public bool Post(ProductPropertyValueModel model)
////		{
////			var entity = new ProductPropertyValueEntity
////			{
////
////				Property = model.PropertyName,
////
////				PropertyValue = model.PropertyValue,
////
////				Product = model.ProductId,
////
////				Adduser = model.Adduser,
////
////				Addtime = model.Addtime,
////
////				UpdUser = model.UpdUser,
////
////				UpdTime = model.UpdTime,
////
////			};
////			if(_ProductPropertyValueService.Create(entity).Id > 0)
////			{
////				return true;
////			}
////			return false;
////		}
//
////		public bool Put(ProductPropertyValueModel model)
////		{
////			var entity = _ProductPropertyValueService.GetProductPropertyValueById(model.Id);
////			if(entity == null)
////				return false;
////
////			entity.Property = model.PropertyName;
////
////			entity.PropertyValue = model.PropertyValue;
////
////			entity.Product = model.ProductId;
////
////			entity.Adduser = model.Adduser;
////
////			entity.Addtime = model.Addtime;
////
////			entity.UpdUser = model.UpdUser;
////
////			entity.UpdTime = model.UpdTime;
////
////			if(_ProductPropertyValueService.Update(entity) != null)
////				return true;
////			return false;
////		}
//
////		public bool Delete(int id)
////		{
////			var entity = _ProductPropertyValueService.GetProductPropertyValueById(id);
////			if(entity == null)
////				return false;
////			if(_ProductPropertyValueService.Delete(entity))
////				return true;
////			return false;
////		}
//	}
//}