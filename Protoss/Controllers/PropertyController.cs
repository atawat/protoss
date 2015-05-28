using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.Property;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class PropertyController : ApiController
	{
		private readonly IPropertyService _PropertyService;

		public PropertyController(IPropertyService PropertyService)
		{
			_PropertyService = PropertyService;
		}

		public PropertyModel Get(int id)
		{
			var entity =_PropertyService.GetPropertyById(id);
			var model = new PropertyModel
			{

				Id = entity.Id

				PropertyName = entity.PropertyName

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				UpdUser = entity.UpdUser

				UpdTime = entity.UpdTime

				Value = entity.Value

			}
			return model;
		}

		public List<PropertyModel> Get(PropertySearchCondtion condition)
		{
			var model = _PropertyService.Get_PropertysByConditon(condition).Select(c=>new _PropertyModel
			{

				Id = entity.Id

				PropertyName = entity.PropertyName

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				UpdUser = entity.UpdUser

				UpdTime = entity.UpdTime

				Value = entity.Value

			});
			return model;
		}

		public bool Post(PropertyModel model)
		{
			var entity = new PropertyEntity
			{

				PropertyName = model.PropertyName

				Adduser = model.Adduser

				Addtime = model.Addtime

				UpdUser = model.UpdUser

				UpdTime = model.UpdTime

				Value = model.Value

			}
			if(_PropertyService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(PropertyModel model)
		{
			var entity = _PropertyService.GetPropertyById(model.Id);
			if(entity == null)
				return false;

			entity.PropertyName = model.PropertyName

			entity.Adduser = model.Adduser

			entity.Addtime = model.Addtime

			entity.UpdUser = model.UpdUser

			entity.UpdTime = model.UpdTime

			entity.Value = model.Value

			if(_PropertyService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _PropertyService.GetPropertyById(id);
			if(entity == null)
				return false;
			if(_PropertyService.Delete(entity))
				return true;
			return false
		}
	}
}