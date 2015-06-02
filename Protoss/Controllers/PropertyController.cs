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
using Protoss.Service.Property;
using YooPoon.Core.Site;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Controllers
{
    [AllowAnonymous]
    [EnableCors("*", "*", "*", SupportsCredentials = true)]
	public class PropertyController : ApiController
	{
		private readonly IPropertyService _propertyService;
	    private readonly IWorkContext _workContext;
        private readonly ICategoryService _categoryService;

        public PropertyController(IPropertyService propertyService,IWorkContext workContext,ICategoryService categoryService)
		{
			_propertyService = propertyService;
		    _workContext = workContext;
	        _categoryService = categoryService;
		}

		public PropertyModel Get(int id)
		{
			var entity =_propertyService.GetPropertyById(id);
		    var model = new PropertyModel
		    {

		        Id = entity.Id,

		        PropertyName = entity.PropertyName,

                Adduser = new UserModel { Id = entity.Adduser.Id, UserName = entity.Adduser.UserName },

		        Addtime = entity.Addtime,

                UpdUser = new UserModel { Id = entity.UpdUser.Id, UserName = entity.UpdUser.UserName },

		        UpdTime = entity.UpdTime,

//		        Value = entity.Value,

                Category = new CategoryModel() { Id = entity.Category.Id}

		    };
			return model;
		}

		public List<PropertyModel> GetByCondition(string PropertyName="",int Page =1,int PageCount =10)
		{
            var condition = new PropertySearchCondition
            {
                Page = Page,
                PageCount = PageCount,
                PropertyName = PropertyName
            };
			var model = _propertyService.GetPropertysByCondition(condition).Select(c=>new PropertyModel
			{

				Id = c.Id,

				PropertyName = c.PropertyName,

                Adduser = new UserModel { Id = c.Adduser.Id, UserName = c.Adduser.UserName },

				Addtime = c.Addtime,

                UpdUser = new UserModel { Id = c.UpdUser.Id, UserName = c.UpdUser.UserName },

				UpdTime = c.UpdTime,

//				Value = c.Value,

			}).ToList();
			return model;
		}

        [HttpGet]
	    public List<PropertyModel> GetByCategoryId(int categoryId)
        {
            var properties = _propertyService.GetPropertyByCategory(categoryId).Select(p=>new PropertyModel
            {
                Id = p.Id,
                Adduser = new UserModel { Id = p.Adduser.Id, UserName = p.Adduser.UserName },
                Addtime = p.Addtime,
                UpdUser = new UserModel { Id = p.UpdUser.Id, UserName = p.UpdUser.UserName },
                UpdTime = p.UpdTime,
                PropertyName = p.PropertyName
            }).ToList();
            return properties;
        }

        public HttpResponseMessage GetCount(string PropertyName = "", int Page = 1, int PageCount = 10)
        {
            var condition = new PropertySearchCondition
            {
                Page = Page,
                PageCount = PageCount,
                PropertyName = PropertyName
            };
            var count = _propertyService.GetPropertyCount(condition);
            return PageHelper.toJson(new { TotalCount = count, Condition = condition });
        }

        
		public bool Post(PropertyModel model)
		{
			var entity = new PropertyEntity
			{

				PropertyName = model.PropertyName,

				Adduser = (UserBase)_workContext.CurrentUser,

                Addtime = DateTime.Now,

                UpdUser = (UserBase)_workContext.CurrentUser,

                UpdTime = DateTime.Now,

//				Value = model.Value,

                Category = _categoryService.GetCategoryById(model.Category.Id)

			};
			if(_propertyService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

        [HttpPost]
		public bool Put(PropertyModel model)
		{
			var entity = _propertyService.GetPropertyById(model.Id);
			if(entity == null)
				return false;

			entity.PropertyName = model.PropertyName;

            entity.UpdUser = (UserBase)_workContext.CurrentUser;

			entity.UpdTime = DateTime.Now;

//			entity.Value = model.Value;

		    entity.Category = _categoryService.GetCategoryById(model.Category.Id);

			if(_propertyService.Update(entity) != null)
				return true;
			return false;
		}

        [HttpGet]
		public bool Delete(int id)
		{
			var entity = _propertyService.GetPropertyById(id);
			if(entity == null)
				return false;
			if(_propertyService.Delete(entity))
				return true;
			return false;
		}
	}
}