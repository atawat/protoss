using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Common;
using Protoss.Entity.Model;
using Protoss.Models;
using Protoss.Service.Category;
using Protoss.Service.Product;
using Protoss.Service.Property;
using Protoss.Service.PropertyValue;
using YooPoon.Core.Site;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Controllers
{
    [AllowAnonymous]
    [EnableCors("*", "*", "*", SupportsCredentials = true)]
	public class ProductController : ApiController
	{
		private readonly IProductService _productService;
        private readonly IWorkContext _workContext;
        private readonly ICategoryService _categoryService;
        private readonly IPropertyService _propertyService;
        private readonly IPropertyValueService _propertyValueService;

        public ProductController(IProductService productService,IWorkContext workContext,
            ICategoryService categoryService,IPropertyService propertyService,IPropertyValueService propertyValueService)
		{
			_productService = productService;
		    _workContext = workContext;
            _categoryService = categoryService;
            _propertyService = propertyService;
            _propertyValueService = propertyValueService;
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

                Image=entity.Image,

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


                Image = c.Image,

//				Detail = c.Detail,

//				Category = c.Category,

				Status = c.Status,

//				PropertyValues = c.PropertyValues,

			}).ToList();
			return model;
		}

        [HttpGet]
        public HttpResponseMessage GetCount(
            int? categoryId = null,
            decimal? priceBegin = null,
            decimal? priceEnd = null,
            bool isDescending = false,
            string name = "",
            string spec = "",
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
                Ids = string.IsNullOrEmpty(ids) ? null : ids.Split(',').Select(int.Parse).ToArray()
            };
            var count = _productService.GetProductCount(condition);
            return PageHelper.toJson(new {TotalCount = count, Condition = condition});
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

                Addtime = DateTime.Now,

                Upduser = user,

				Updtime = DateTime.Now,

				Unit = model.Unit,

                Image = model.Image,

				Detail = new ProductDetailEntity
				{
				    Detail = model.Detail.Detail,
                    ImgUrl1 = model.Detail.ImgUrl1,
                    ImgUrl2 = model.Detail.ImgUrl2,
                    ImgUrl3 = model.Detail.ImgUrl3,
                    ImgUrl4 = model.Detail.ImgUrl4,
                    ImgUrl5 = model.Detail.ImgUrl5,
				},

				Category = _categoryService.GetCategoryById(model.Category.Id),

				Status = EnumProductStatus.OnSale,

				PropertyValues = model.PropertyValues.Select(pv=>new ProductPropertyValueEntity
				{
				    Addtime = DateTime.Now,
                    Adduser = (UserBase)_workContext.CurrentUser,
                    UpdTime = DateTime.Now,
                    UpdUser = (UserBase)_workContext.CurrentUser,
                    Property = _propertyService.GetPropertyById(pv.PropertyId),
                    PropertyValue = _propertyValueService.GetOrCreatEntityWithValue(pv.PropertyValue,pv.PropertyId)
				}).ToList(),

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

            entity.Image = model.Image;


		    entity.Detail.Detail = model.Detail.Detail;
		    entity.Detail.ImgUrl1 = model.Detail.ImgUrl1;
		    entity.Detail.ImgUrl2 = model.Detail.ImgUrl2;
		    entity.Detail.ImgUrl3 = model.Detail.ImgUrl3;
		    entity.Detail.ImgUrl4 = model.Detail.ImgUrl4;
		    entity.Detail.ImgUrl5 = model.Detail.ImgUrl5;


			entity.Category = _categoryService.GetCategoryById(model.Category.Id);

			entity.Status = model.Status;

		    foreach (var pv in entity.PropertyValues)
		    {
		        pv.PropertyValue.Value = model.PropertyValues.First(c => c.PropertyId == pv.Property.Id).PropertyValue;
		    }

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