using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.PropertyValue;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Controllers
{
    [EnableCors("*", "*", "*", SupportsCredentials = true)]
	public class PropertyValueController : ApiController
	{
		private readonly IPropertyValueService _propertyValueService;
	    private readonly IWorkContext _workContext;

	    public PropertyValueController(IPropertyValueService propertyValueService,IWorkContext workContext)
		{
			_propertyValueService = propertyValueService;
		    _workContext = workContext;
		}

		public PropertyValueModel Get(int id)
		{
			var entity =_propertyValueService.GetPropertyValueById(id);
		    var model = new PropertyValueModel
		    {

		        Id = entity.Id,

//		        Property = entity.Property,

		        Value = entity.Value,

                Adduser = new UserModel { Id = entity.Adduser.Id, UserName = entity.Adduser.UserName },

		        Addtime = entity.Addtime,

                UpdUser = new UserModel { Id = entity.UpdUser.Id, UserName = entity.UpdUser.UserName },

		        UpdTime = entity.UpdTime,

		    };
			return model;
		}

		public List<PropertyValueModel> Get(PropertyValueSearchCondition condition)
		{
			var model = _propertyValueService.GetPropertyValuesByCondition(condition).Select(c=>new PropertyValueModel
			{

				Id = c.Id,

//				Property = c.Property,

				Value = c.Value,

                Adduser = new UserModel { Id = c.Adduser.Id, UserName = c.Adduser.UserName },

				Addtime = c.Addtime,

                UpdUser = new UserModel { Id = c.UpdUser.Id, UserName = c.UpdUser.UserName },

				UpdTime = c.UpdTime,

			}).ToList();
			return model;
		}

		public bool Post(PropertyValueModel model)
		{
			var entity = new PropertyValueEntity
			{

//				Property = model.Property,

				Value = model.Value,

				Adduser = (UserBase)_workContext.CurrentUser,

                Addtime = DateTime.Now,

                UpdUser = (UserBase)_workContext.CurrentUser,

                UpdTime = DateTime.Now,

			};
			if(_propertyValueService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(PropertyValueModel model)
		{
			var entity = _propertyValueService.GetPropertyValueById(model.Id);
			if(entity == null)
				return false;

//			entity.Property = model.Property;

			entity.Value = model.Value;

            entity.UpdUser = (UserBase)_workContext.CurrentUser;

			entity.UpdTime = DateTime.Now;

			if(_propertyValueService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _propertyValueService.GetPropertyValueById(id);
			if(entity == null)
				return false;
			if(_propertyValueService.Delete(entity))
				return true;
			return false;
		}
	}
}