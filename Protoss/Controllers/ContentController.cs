using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.Content;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class ContentController : ApiController
	{
		private readonly IContentService _ContentService;

		public ContentController(IContentService ContentService)
		{
			_ContentService = ContentService;
		}

		public ContentModel Get(int id)
		{
			var entity =_ContentService.GetContentById(id);
			var model = new ContentModel
			{

				Id = entity.Id

				Content = entity.Content

				Title = entity.Title

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				Upduser = entity.Upduser

				Updtime = entity.Updtime

				Status = entity.Status

				Praise = entity.Praise

				Unpraise = entity.Unpraise

				Viewcount = entity.Viewcount

				Tags = entity.Tags

				Channels = entity.Channels

			}
			return model;
		}

		public List<ContentModel> Get(ContentSearchCondtion condition)
		{
			var model = _ContentService.Get_ContentsByConditon(condition).Select(c=>new _ContentModel
			{

				Id = entity.Id

				Content = entity.Content

				Title = entity.Title

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				Upduser = entity.Upduser

				Updtime = entity.Updtime

				Status = entity.Status

				Praise = entity.Praise

				Unpraise = entity.Unpraise

				Viewcount = entity.Viewcount

				Tags = entity.Tags

				Channels = entity.Channels

			});
			return model;
		}

		public bool Post(ContentModel model)
		{
			var entity = new ContentEntity
			{

				Content = model.Content

				Title = model.Title

				Adduser = model.Adduser

				Addtime = model.Addtime

				Upduser = model.Upduser

				Updtime = model.Updtime

				Status = model.Status

				Praise = model.Praise

				Unpraise = model.Unpraise

				Viewcount = model.Viewcount

				Tags = model.Tags

				Channels = model.Channels

			}
			if(_ContentService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(ContentModel model)
		{
			var entity = _ContentService.GetContentById(model.Id);
			if(entity == null)
				return false;

			entity.Content = model.Content

			entity.Title = model.Title

			entity.Adduser = model.Adduser

			entity.Addtime = model.Addtime

			entity.Upduser = model.Upduser

			entity.Updtime = model.Updtime

			entity.Status = model.Status

			entity.Praise = model.Praise

			entity.Unpraise = model.Unpraise

			entity.Viewcount = model.Viewcount

			entity.Tags = model.Tags

			entity.Channels = model.Channels

			if(_ContentService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _ContentService.GetContentById(id);
			if(entity == null)
				return false;
			if(_ContentService.Delete(entity))
				return true;
			return false
		}
	}
}