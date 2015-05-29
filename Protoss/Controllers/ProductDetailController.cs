using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.ProductDetail;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class ProductDetailController : ApiController
	{
		private readonly IProductDetailService _ProductDetailService;

		public ProductDetailController(IProductDetailService ProductDetailService)
		{
			_ProductDetailService = ProductDetailService;
		}

		public ProductDetailModel Get(int id)
		{
			var entity =_ProductDetailService.GetProductDetailById(id);
		    var model = new ProductDetailModel
		    {

		        Id = entity.Id,

		        Detail = entity.Detail,

		        ImgUrl1 = entity.ImgUrl1,

		        ImgUrl2 = entity.ImgUrl2,

		        ImgUrl3 = entity.ImgUrl3,

		        ImgUrl4 = entity.ImgUrl4,

		        ImgUrl5 = entity.ImgUrl5,

//		        Product = entity.Product,

		    };
			return model;
		}

		public List<ProductDetailModel> Get(ProductDetailSearchCondition condition)
		{
			var model = _ProductDetailService.GetProductDetailsByCondition(condition).Select(c=>new ProductDetailModel
			{

				Id = c.Id,

				Detail = c.Detail,

				ImgUrl1 = c.ImgUrl1,

				ImgUrl2 = c.ImgUrl2,

				ImgUrl3 = c.ImgUrl3,

				ImgUrl4 = c.ImgUrl4,

				ImgUrl5 = c.ImgUrl5,

//				Product = c.Product,

			}).ToList();
			return model;
		}

		public bool Post(ProductDetailModel model)
		{
			var entity = new ProductDetailEntity
			{

				Detail = model.Detail,

				ImgUrl1 = model.ImgUrl1,

				ImgUrl2 = model.ImgUrl2,

				ImgUrl3 = model.ImgUrl3,

				ImgUrl4 = model.ImgUrl4,

				ImgUrl5 = model.ImgUrl5,

//				Product = model.Product,

			};
			if(_ProductDetailService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(ProductDetailModel model)
		{
			var entity = _ProductDetailService.GetProductDetailById(model.Id);
			if(entity == null)
				return false;

			entity.Detail = model.Detail;

			entity.ImgUrl1 = model.ImgUrl1;

			entity.ImgUrl2 = model.ImgUrl2;

			entity.ImgUrl3 = model.ImgUrl3;

			entity.ImgUrl4 = model.ImgUrl4;

			entity.ImgUrl5 = model.ImgUrl5;

//			entity.Product = model.Product;

			if(_ProductDetailService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _ProductDetailService.GetProductDetailById(id);
			if(entity == null)
				return false;
			if(_ProductDetailService.Delete(entity))
				return true;
			return false;
		}
	}
}