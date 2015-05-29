using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.Member;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class MemberController : ApiController
	{
		private readonly IMemberService _MemberService;

		public MemberController(IMemberService MemberService)
		{
			_MemberService = MemberService;
		}

		public MemberModel Get(int id)
		{
			var entity =_MemberService.GetMemberById(id);
		    var model = new MemberModel
		    {

		        Id = entity.Id,

		        User = entity.User,

		        OpenId = entity.OpenId,

//		        Orders = entity.Orders,

		        ContactName = entity.ContactName,

		    };
			return model;
		}

		public List<MemberModel> Get(MemberSearchCondition condition)
		{
			var model = _MemberService.GetMembersByCondition(condition).Select(c=>new MemberModel
			{

				Id = c.Id,

				User = c.User,

				OpenId = c.OpenId,

//				Orders = c.Orders,

				ContactName = c.ContactName,

			}).ToList();
			return model;
		}

		public bool Post(MemberModel model)
		{
			var entity = new MemberEntity
			{

				User = model.User,

				OpenId = model.OpenId,

//				Orders = model.Orders,

				ContactName = model.ContactName,

			};
			if(_MemberService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(MemberModel model)
		{
			var entity = _MemberService.GetMemberById(model.Id);
			if(entity == null)
				return false;

			entity.User = model.User;

			entity.OpenId = model.OpenId;

//			entity.Orders = model.Orders;

			entity.ContactName = model.ContactName;

			if(_MemberService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _MemberService.GetMemberById(id);
			if(entity == null)
				return false;
			if(_MemberService.Delete(entity))
				return true;
			return false;
		}
	}
}