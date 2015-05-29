using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.Product;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class ProductController : ApiController
	{
		private readonly IProductService _ProductService;

		public ProductController(IProductService ProductService)
		{
			_ProductService = ProductService;
		}

		public ProductModel Get(int id)
		{
			var entity =_ProductService.GetProductById(id);
			var model = new ProductModel
			{

				Id = entity.Id

				Name = entity.Name

				Spec = entity.Spec

				Price = entity.Price

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				Upduser = entity.Upduser

				Updtime = entity.Updtime

				Unit = entity.Unit

				Detail = entity.Detail

				Category = entity.Category

				Status = entity.Status

				PropertyValues = entity.PropertyValues

			}
			return model;
		}

		public List<ProductModel> Get(ProductSearchCondtion condition)
		{
			var model = _ProductService.Get_ProductsByConditon(condition).Select(c=>new _ProductModel
			{

				Id = entity.Id

				Name = entity.Name

				Spec = entity.Spec

				Price = entity.Price

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				Upduser = entity.Upduser

				Updtime = entity.Updtime

				Unit = entity.Unit

				Detail = entity.Detail

				Category = entity.Category

				Status = entity.Status

				PropertyValues = entity.PropertyValues

			});
			return model;
		}

		public bool Post(ProductModel model)
		{
			var entity = new ProductEntity
			{

				Name = model.Name

				Spec = model.Spec

				Price = model.Price

				Adduser = model.Adduser

				Addtime = model.Addtime

				Upduser = model.Upduser

				Updtime = model.Updtime

				Unit = model.Unit

				Detail = model.Detail

				Category = model.Category

				Status = model.Status

				PropertyValues = model.PropertyValues

			}
			if(_ProductService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(ProductModel model)
		{
			var entity = _ProductService.GetProductById(model.Id);
			if(entity == null)
				return false;

			entity.Name = model.Name

			entity.Spec = model.Spec

			entity.Price = model.Price

			entity.Adduser = model.Adduser

			entity.Addtime = model.Addtime

			entity.Upduser = model.Upduser

			entity.Updtime = model.Updtime

			entity.Unit = model.Unit

			entity.Detail = model.Detail

			entity.Category = model.Category

			entity.Status = model.Status

			entity.PropertyValues = model.PropertyValues

			if(_ProductService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _ProductService.GetProductById(id);
			if(entity == null)
				return false;
			if(_ProductService.Delete(entity))
				return true;
			return false
		}
	}
}