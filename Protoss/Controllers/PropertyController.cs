using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Protoss.Entity.Model;
using Protoss.Models;
using Protoss.Service.Property;
using YooPoon.Core.Site;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Controllers
{
	public class PropertyController : ApiController
	{
		private readonly IPropertyService _propertyService;
	    private readonly IWorkContext _workContext;

	    public PropertyController(IPropertyService propertyService,IWorkContext workContext)
		{
			_propertyService = propertyService;
		    _workContext = workContext;
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

		    };
			return model;
		}

		public List<PropertyModel> Get(PropertySearchCondition condition)
		{
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

			};
			if(_propertyService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(PropertyModel model)
		{
			var entity = _propertyService.GetPropertyById(model.Id);
			if(entity == null)
				return false;

			entity.PropertyName = model.PropertyName;

            entity.UpdUser = (UserBase)_workContext.CurrentUser;

			entity.UpdTime = DateTime.Now;

//			entity.Value = model.Value;

			if(_propertyService.Update(entity) != null)
				return true;
			return false;
		}

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