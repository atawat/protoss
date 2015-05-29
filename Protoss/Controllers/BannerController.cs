using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.Banner;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class BannerController : ApiController
	{
		private readonly IBannerService _BannerService;

		public BannerController(IBannerService BannerService)
		{
			_BannerService = BannerService;
		}

		public BannerModel Get(int id)
		{
			var entity =_BannerService.GetBannerById(id);
		    var model = new BannerModel
		    {

		        Id = entity.Id,

		        Title = entity.Title,

		        ImgUrl = entity.ImgUrl,

		        Order = entity.Order,

		        Adduser = entity.Adduser,

		        Addtime = entity.Addtime,

		        Upduser = entity.Upduser,

		        Updtime = entity.Updtime,

//		        Content = entity.Content,

		    };
			return model;
		}

		public List<BannerModel> Get(BannerSearchCondition condition)
		{
			var model = _BannerService.GetBannersByCondition(condition).Select(c=>new BannerModel
			{

				Id = c.Id,

				Title = c.Title,

				ImgUrl = c.ImgUrl,

				Order = c.Order,

				Adduser = c.Adduser,

				Addtime = c.Addtime,

				Upduser = c.Upduser,

				Updtime = c.Updtime,

//				Content = c.Content,

			}).ToList();
			return model;
		}

		public bool Post(BannerModel model)
		{
			var entity = new BannerEntity
			{

				Title = model.Title,

				ImgUrl = model.ImgUrl,

				Order = model.Order,

				Adduser = model.Adduser,

				Addtime = model.Addtime,

				Upduser = model.Upduser,

				Updtime = model.Updtime,

//				Content = model.Content,

			};
			if(_BannerService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(BannerModel model)
		{
			var entity = _BannerService.GetBannerById(model.Id);
			if(entity == null)
				return false;

			entity.Title = model.Title;

			entity.ImgUrl = model.ImgUrl;

			entity.Order = model.Order;

			entity.Adduser = model.Adduser;

			entity.Addtime = model.Addtime;

			entity.Upduser = model.Upduser;

			entity.Updtime = model.Updtime;

//			entity.Content = model.Content;

			if(_BannerService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _BannerService.GetBannerById(id);
			if(entity == null)
				return false;
			if(_BannerService.Delete(entity))
				return true;
			return false;
		}
	}
}