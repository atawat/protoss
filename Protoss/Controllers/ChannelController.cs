using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.Channel;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class ChannelController : ApiController
	{
		private readonly IChannelService _ChannelService;

		public ChannelController(IChannelService ChannelService)
		{
			_ChannelService = ChannelService;
		}

		public ChannelModel Get(int id)
		{
			var entity =_ChannelService.GetChannelById(id);
		    var model = new ChannelModel
		    {

		        Id = entity.Id,

		        Name = entity.Name,

		        Status = entity.Status,

//		        Parent = entity.Parent,

		        Adduser = entity.Adduser,

		        Addtime = entity.Addtime,

		        Upduser = entity.Upduser,

		        Updtime = entity.Updtime,

		    };
			return model;
		}

		public List<ChannelModel> Get(ChannelSearchCondition condition)
		{
			var model = _ChannelService.GetChannelsByCondition(condition).Select(c=>new ChannelModel
			{

				Id = c.Id,

				Name = c.Name,

				Status = c.Status,

//				Parent = c.Parent,

				Adduser = c.Adduser,

				Addtime = c.Addtime,

				Upduser = c.Upduser,

				Updtime = c.Updtime,

			}).ToList();
			return model;
		}

		public bool Post(ChannelModel model)
		{
			var entity = new ChannelEntity
			{

				Name = model.Name,

				Status = model.Status,

//				Parent = model.Parent,

				Adduser = model.Adduser,

				Addtime = model.Addtime,

				Upduser = model.Upduser,

				Updtime = model.Updtime,

			};
			if(_ChannelService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(ChannelModel model)
		{
			var entity = _ChannelService.GetChannelById(model.Id);
			if(entity == null)
				return false;

			entity.Name = model.Name;

			entity.Status = model.Status;

//			entity.Parent = model.Parent;

			entity.Adduser = model.Adduser;

			entity.Addtime = model.Addtime;

			entity.Upduser = model.Upduser;

			entity.Updtime = model.Updtime;

			if(_ChannelService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _ChannelService.GetChannelById(id);
			if(entity == null)
				return false;
			if(_ChannelService.Delete(entity))
				return true;
			return false;
		}
	}
}