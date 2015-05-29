using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.ProductPropertyValue;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class ProductPropertyValueController : ApiController
	{
		private readonly IProductPropertyValueService _ProductPropertyValueService;

		public ProductPropertyValueController(IProductPropertyValueService ProductPropertyValueService)
		{
			_ProductPropertyValueService = ProductPropertyValueService;
		}

		public ProductPropertyValueModel Get(int id)
		{
			var entity =_ProductPropertyValueService.GetProductPropertyValueById(id);
			var model = new ProductPropertyValueModel
			{

				Id = entity.Id

				Property = entity.Property

				PropertyValue = entity.PropertyValue

				Product = entity.Product

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				UpdUser = entity.UpdUser

				UpdTime = entity.UpdTime

			}
			return model;
		}

		public List<ProductPropertyValueModel> Get(ProductPropertyValueSearchCondtion condition)
		{
			var model = _ProductPropertyValueService.Get_ProductPropertyValuesByConditon(condition).Select(c=>new _ProductPropertyValueModel
			{

				Id = entity.Id

				Property = entity.Property

				PropertyValue = entity.PropertyValue

				Product = entity.Product

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				UpdUser = entity.UpdUser

				UpdTime = entity.UpdTime

			});
			return model;
		}

		public bool Post(ProductPropertyValueModel model)
		{
			var entity = new ProductPropertyValueEntity
			{

				Property = model.Property

				PropertyValue = model.PropertyValue

				Product = model.Product

				Adduser = model.Adduser

				Addtime = model.Addtime

				UpdUser = model.UpdUser

				UpdTime = model.UpdTime

			}
			if(_ProductPropertyValueService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(ProductPropertyValueModel model)
		{
			var entity = _ProductPropertyValueService.GetProductPropertyValueById(model.Id);
			if(entity == null)
				return false;

			entity.Property = model.Property

			entity.PropertyValue = model.PropertyValue

			entity.Product = model.Product

			entity.Adduser = model.Adduser

			entity.Addtime = model.Addtime

			entity.UpdUser = model.UpdUser

			entity.UpdTime = model.UpdTime

			if(_ProductPropertyValueService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _ProductPropertyValueService.GetProductPropertyValueById(id);
			if(entity == null)
				return false;
			if(_ProductPropertyValueService.Delete(entity))
				return true;
			return false
		}
	}
}