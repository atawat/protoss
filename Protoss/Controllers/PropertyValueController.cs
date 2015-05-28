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

namespace Protoss.Controllers
{
	public class PropertyValueController : ApiController
	{
		private readonly IPropertyValueService _PropertyValueService;

		public PropertyValueController(IPropertyValueService PropertyValueService)
		{
			_PropertyValueService = PropertyValueService;
		}

		public PropertyValueModel Get(int id)
		{
			var entity =_PropertyValueService.GetPropertyValueById(id);
			var model = new PropertyValueModel
			{

				Id = entity.Id

				Property = entity.Property

				Value = entity.Value

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				UpdUser = entity.UpdUser

				UpdTime = entity.UpdTime

			}
			return model;
		}

		public List<PropertyValueModel> Get(PropertyValueSearchCondtion condition)
		{
			var model = _PropertyValueService.Get_PropertyValuesByConditon(condition).Select(c=>new _PropertyValueModel
			{

				Id = entity.Id

				Property = entity.Property

				Value = entity.Value

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				UpdUser = entity.UpdUser

				UpdTime = entity.UpdTime

			});
			return model;
		}

		public bool Post(PropertyValueModel model)
		{
			var entity = new PropertyValueEntity
			{

				Property = model.Property

				Value = model.Value

				Adduser = model.Adduser

				Addtime = model.Addtime

				UpdUser = model.UpdUser

				UpdTime = model.UpdTime

			}
			if(_PropertyValueService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(PropertyValueModel model)
		{
			var entity = _PropertyValueService.GetPropertyValueById(model.Id);
			if(entity == null)
				return false;

			entity.Property = model.Property

			entity.Value = model.Value

			entity.Adduser = model.Adduser

			entity.Addtime = model.Addtime

			entity.UpdUser = model.UpdUser

			entity.UpdTime = model.UpdTime

			if(_PropertyValueService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _PropertyValueService.GetPropertyValueById(id);
			if(entity == null)
				return false;
			if(_PropertyValueService.Delete(entity))
				return true;
			return false
		}
	}
}