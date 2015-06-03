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

        public ProductController(IProductService productService, IWorkContext workContext,
            ICategoryService categoryService, IPropertyService propertyService, IPropertyValueService propertyValueService)
        {
            _productService = productService;
            _workContext = workContext;
            _categoryService = categoryService;
            _propertyService = propertyService;
            _propertyValueService = propertyValueService;
        }

        public ProductModel Get(int id)
        {
            var entity = _productService.GetProductById(id);
            var model = entity == null ? new ProductModel() : new ProductModel
            {

                Id = entity.Id,

                Name = entity.Name,

                Spec = entity.Spec,

                Price = entity.Price,

                Adduser = new UserModel { Id = entity.Adduser.Id, UserName = entity.Adduser.UserName },

                Addtime = entity.Addtime,

                Upduser = new UserModel { Id = entity.Upduser.Id, UserName = entity.Upduser.UserName },

                Updtime = entity.Updtime,

                Unit = entity.Unit,

                Image = entity.Image,

                Detail = new ProductDetailModel
                {
                    Detail = entity.Detail.Detail,
                    ImgUrl1 = entity.Detail.ImgUrl1,
                    ImgUrl2 = entity.Detail.ImgUrl2,
                    ImgUrl3 = entity.Detail.ImgUrl3,
                    ImgUrl4 = entity.Detail.ImgUrl4,
                    ImgUrl5 = entity.Detail.ImgUrl5
                },

                Category = new CategoryModel
                {
                    CategoryName = entity.Category.CategoryName,
                    Id = entity.Category.Id
                },

                Status = entity.Status,

                PropertyValues = entity.PropertyValues.GroupBy(pv => pv.Property).Select(g => new ProductPropertyValueModel
                {
                    Id = g.Key.Id,
                    PropertyName = g.Key.PropertyName,
                    PropertyValues = g.Select(v => new PropertyValueModel
                    {
                        Id = v.PropertyValue.Id,
                        Value = v.PropertyValue.Value
                    }).ToList(),
                    ProductId = entity.Id
                }).ToList(),

            };
            return model;
        }

        [HttpGet]
        public List<ProductModel> GetByCondition(
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
            var model = _productService.GetProductsByCondition(condition).Select(c => new ProductModel
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
            return PageHelper.toJson(new { TotalCount = count, Condition = condition });
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

                //				PropertyValues = model.PropertyValues.SelectMany(pv=>new ProductPropertyValueEntity
                //				{
                //				    Addtime = DateTime.Now,
                //                    Adduser = (UserBase)_workContext.CurrentUser,
                //                    UpdTime = DateTime.Now,
                //                    UpdUser = (UserBase)_workContext.CurrentUser,
                //                    Property = _propertyService.GetPropertyById(pv.PropertyId),
                //                    PropertyValue = _propertyValueService.GetOrCreatEntityWithValue(pv.PropertyValue, _propertyService.GetPropertyById(pv.PropertyId))
                //				}).ToList()

            };
            var productProperty = (from pv in model.PropertyValues
                let p = _propertyService.GetPropertyById(pv.PropertyId)
                from v in pv.PropertyValues
                select new ProductPropertyValueEntity
                {
                    Addtime = DateTime.Now, 
                    Adduser = (UserBase) _workContext.CurrentUser,
                    UpdTime = DateTime.Now,
                    UpdUser = (UserBase) _workContext.CurrentUser, 
                    Property = p,
                    PropertyValue = _propertyValueService.GetOrCreatEntityWithValue(v.Value, p)
                }).ToList();
            entity.PropertyValues = productProperty;
            return _productService.Create(entity).Id > 0;
        }
        [HttpPost]
        public bool Put(ProductModel model)
        {
            var curruentUser = (UserBase)_workContext.CurrentUser;
            var nowTime = DateTime.Now;

			var entity = _productService.GetProductById(model.Id);
			if(entity == null)
				return false;

		    var user = curruentUser;

			entity.Name = model.Name;

			entity.Spec = model.Spec;

			entity.Price = model.Price;

			entity.Upduser = user;

            entity.Updtime = nowTime;

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

		    foreach (var p in model.PropertyValues)
		    {
		        foreach (var pv in p.PropertyValues)
		        {
		            if (pv.Id != 0)
		                entity.PropertyValues.First(pp => pp.PropertyValue.Id == pv.Id).PropertyValue.Value = pv.Value;
		            else
		            {
		                var property = entity.PropertyValues.First(pp => pp.Property.Id == p.PropertyId).Property;
                        var newPropertyValue = new ProductPropertyValueEntity()
                        {
                            Addtime = nowTime,
                            Adduser = curruentUser,
                            UpdTime = nowTime,
                            UpdUser = curruentUser,
                            Property = property,
                            PropertyValue = new PropertyValueEntity
                            {
                                Addtime = nowTime,
                                Adduser = curruentUser,
                                UpdTime = nowTime,
                                UpdUser = curruentUser,
                                Value = pv.Value,
                                Property = property
                            }
                        };
		                entity.PropertyValues.Add(newPropertyValue);
		            }
		        }
//		        pv.PropertyValue.Value = model.PropertyValues.First(c => c.PropertyId == pv.Property.Id).PropertyValues.First(v=>v.Id);
		    }

			if(_productService.Update(entity) != null)
				return true;
			return false;
		}

        [HttpGet]
        public bool Delete(int id)
        {
            var entity = _productService.GetProductById(id);
            if (entity == null)
                return false;
            if (_productService.Delete(entity))
                return true;
            return false;
        }
    }
}