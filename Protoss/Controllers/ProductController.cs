using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Protoss.Entity.Model;
using Protoss.Models;
using Protoss.Service.Product;
using YooPoon.Core.Site;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Controllers
{
    [AllowAnonymous]
	public class ProductController : ApiController
	{
		private readonly IProductService _productService;
        private readonly IWorkContext _workContext;

        public ProductController(IProductService productService,IWorkContext workContext)
		{
			_productService = productService;
		    _workContext = workContext;
		}

		public ProductModel Get(int id)
		{
			var entity =_productService.GetProductById(id);
		    var model = entity==null?new ProductModel() : new ProductModel
		    {

		        Id = entity.Id,

		        Name = entity.Name,

		        Spec = entity.Spec,

		        Price = entity.Price,

		        Adduser = new UserModel{Id = entity.Adduser.Id,UserName = entity.Adduser.UserName},

		        Addtime = entity.Addtime,

                Upduser = new UserModel { Id = entity.Upduser.Id, UserName = entity.Upduser.UserName },

		        Updtime = entity.Updtime,

		        Unit = entity.Unit,

                imge=entity.imge,

//		        Detail = entity.Detail,

//		        Category = entity.Category,

		        Status = entity.Status,

		        PropertyValues = entity.PropertyValues.Select(c=>new ProductPropertyValueModel{Id = c.Id,PropertyName = c.Property.PropertyName,PropertyValue = c.PropertyValue.Value,ProductId = c.Product.Id}).ToList(),

		    };
			return model;
		}

        [HttpGet]
        public List<ProductModel> GetByCondition(
            int? categoryId = null, 
            decimal? priceBegin = null, 
            decimal? priceEnd = null,
            bool isDescending =false,
            string name ="",
            string spec ="", 
            int pageCount = 10, 
            int page = 1, 
            string ids = "",
            EnumProductSearchOrderBy orderBy = EnumProductSearchOrderBy.OrderById)
		{
            var condition = new ProductSearchCondition
            {
                CategoryId = categoryId,
                IsDescending = isDescending,
                Name = name,
                OrderBy = orderBy,
                Page = page,
                PageCount = pageCount,
                PriceBegin = priceBegin,
                PriceEnd = priceEnd,
                Spec = spec,
                Ids = string.IsNullOrEmpty(ids)?null:ids.Split(',').Select(int.Parse).ToArray()
            };
			var model = _productService.GetProductsByCondition(condition).Select(c=>new ProductModel
			{

				Id = c.Id,

				Name = c.Name,

				Spec = c.Spec,

				Price = c.Price,

                Adduser = new UserModel { Id = c.Adduser.Id, UserName = c.Adduser.UserName },

                Addtime = c.Addtime,

                Upduser = new UserModel { Id = c.Upduser.Id, UserName = c.Upduser.UserName },

				Updtime = c.Updtime,

				Unit = c.Unit,


                imge = c.imge,

//				Detail = c.Detail,

//				Category = c.Category,

				Status = c.Status,

//				PropertyValues = c.PropertyValues,

			}).ToList();
			return model;
		}

		public bool Post(ProductModel model)
		{
		    var user = (UserBase)_workContext.CurrentUser;
			var entity = new ProductEntity
			{

				Name = model.Name,

				Spec = model.Spec,

				Price = model.Price,

                Adduser = user,

                Addtime = model.Addtime,

                Upduser = user,

				Updtime = model.Updtime,

				Unit = model.Unit,

                imge = model.imge,

//				Detail = model.Detail,

//				Category = model.Category,

				Status = model.Status,

//				PropertyValues = model.PropertyValues,

			};
			if(_productService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(ProductModel model)
		{
			var entity = _productService.GetProductById(model.Id);
			if(entity == null)
				return false;

		    var user = (UserBase) _workContext.CurrentUser;

			entity.Name = model.Name;

			entity.Spec = model.Spec;

			entity.Price = model.Price;

			entity.Upduser = user;

			entity.Updtime = model.Updtime;

			entity.Unit = model.Unit;

            entity.imge = model.imge;

//			entity.Detail = model.Detail;

//			entity.Category = model.Category;

			entity.Status = model.Status;

//			entity.PropertyValues = model.PropertyValues;

			if(_productService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _productService.GetProductById(id);
			if(entity == null)
				return false;
			if(_productService.Delete(entity))
				return true;
			return false;
		}
	}
}