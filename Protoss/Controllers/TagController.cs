using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.Tag;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class TagController : ApiController
	{
		private readonly ITagService _TagService;

		public TagController(ITagService TagService)
		{
			_TagService = TagService;
		}

		public TagModel Get(int id)
		{
			var entity =_TagService.GetTagById(id);
			var model = new TagModel
			{

				Id = entity.Id

				Tag = entity.Tag

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				UpdUser = entity.UpdUser

				UpdTime = entity.UpdTime

				Content = entity.Content

			}
			return model;
		}

		public List<TagModel> Get(TagSearchCondtion condition)
		{
			var model = _TagService.Get_TagsByConditon(condition).Select(c=>new _TagModel
			{

				Id = entity.Id

				Tag = entity.Tag

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				UpdUser = entity.UpdUser

				UpdTime = entity.UpdTime

				Content = entity.Content

			});
			return model;
		}

		public bool Post(TagModel model)
		{
			var entity = new TagEntity
			{

				Tag = model.Tag

				Adduser = model.Adduser

				Addtime = model.Addtime

				UpdUser = model.UpdUser

				UpdTime = model.UpdTime

				Content = model.Content

			}
			if(_TagService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(TagModel model)
		{
			var entity = _TagService.GetTagById(model.Id);
			if(entity == null)
				return false;

			entity.Tag = model.Tag

			entity.Adduser = model.Adduser

			entity.Addtime = model.Addtime

			entity.UpdUser = model.UpdUser

			entity.UpdTime = model.UpdTime

			entity.Content = model.Content

			if(_TagService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _TagService.GetTagById(id);
			if(entity == null)
				return false;
			if(_TagService.Delete(entity))
				return true;
			return false
		}
	}
}