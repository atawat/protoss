using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.Channel ;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class Channel Controller : ApiController
	{
		private readonly IChannel Service _Channel Service;

		public Channel Controller(IChannel Service Channel Service)
		{
			_Channel Service = Channel Service;
		}

		public Channel Model Get(int id)
		{
			var entity =_Channel Service.GetChannel ById(id);
			var model = new Channel Model
			{

				Id = entity.Id

				Name = entity.Name

				Status = entity.Status

				Parent = entity.Parent

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				Upduser = entity.Upduser

				Updtime = entity.Updtime

			}
			return model;
		}

		public List<Channel Model> Get(Channel SearchCondtion condition)
		{
			var model = _Channel Service.Get_Channel sByConditon(condition).Select(c=>new _Channel Model
			{

				Id = entity.Id

				Name = entity.Name

				Status = entity.Status

				Parent = entity.Parent

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				Upduser = entity.Upduser

				Updtime = entity.Updtime

			});
			return model;
		}

		public bool Post(Channel Model model)
		{
			var entity = new Channel Entity
			{

				Name = model.Name

				Status = model.Status

				Parent = model.Parent

				Adduser = model.Adduser

				Addtime = model.Addtime

				Upduser = model.Upduser

				Updtime = model.Updtime

			}
			if(_Channel Service.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(Channel Model model)
		{
			var entity = _Channel Service.GetChannel ById(model.Id);
			if(entity == null)
				return false;

			entity.Name = model.Name

			entity.Status = model.Status

			entity.Parent = model.Parent

			entity.Adduser = model.Adduser

			entity.Addtime = model.Addtime

			entity.Upduser = model.Upduser

			entity.Updtime = model.Updtime

			if(_Channel Service.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _Channel Service.GetChannel ById(id);
			if(entity == null)
				return false;
			if(_Channel Service.Delete(entity))
				return true;
			return false
		}
	}
}