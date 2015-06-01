using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Common;
using Protoss.Entity.Model;
using Protoss.Service.Channel;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Controllers
{
    [EnableCors("*", "*", "*", SupportsCredentials = true)]
	public class ChannelController : ApiController
	{
		private readonly IChannelService _ChannelService;
        private readonly IWorkContext _workContext;

        public ChannelController(IChannelService ChannelService,IWorkContext workContext)
		{
			_ChannelService = ChannelService;
		    _workContext = workContext;
		}
        [EnableCors("*", "*", "*", SupportsCredentials = true)]
		public ChannelModel Get(int id)
		{
			var entity =_ChannelService.GetChannelById(id);
		    var model = new ChannelModel
		    {

		        Id = entity.Id,

		        Name = entity.Name,

		        Status = entity.Status,

//		        Parent = entity.Parent,

//		        Adduser = entity.Adduser,
//
//		        Addtime = entity.Addtime,
//
//		        Upduser = entity.Upduser,
//
//		        Updtime = entity.Updtime,

		    };
			return model;
		}
        [EnableCors("*", "*", "*", SupportsCredentials = true)]
		public List<ChannelModel> GetByCondition(
            int page = 1,
            int pageCount=10,
            bool isDescending = false,
            string name = "",
            EnumChannelStatus? status = null,
            EnumChannelSearchOrderBy orderBy = EnumChannelSearchOrderBy.OrderById)
		{
            var condition = new ChannelSearchCondition
            {
                IsDescending = isDescending,
                Page = page,
                PageCount = pageCount,
                Name = name,
                Status = status,
                OrderBy = orderBy
            };
			var model = _ChannelService.GetChannelsByCondition(condition).Select(c=>new ChannelModel
			{

				Id = c.Id,

				Name = c.Name,

				Status = c.Status,

//				Parent = c.Parent,

//				Adduser = c.Adduser,
//
//				Addtime = c.Addtime,
//
//				Upduser = c.Upduser,
//
//				Updtime = c.Updtime,

			}).ToList();
			return model;
		}

        [EnableCors("*", "*", "*", SupportsCredentials = true)]
	    public HttpResponseMessage GetCount(
	        int page = 1,
	        int pageCount = 10,
	        bool isDescending = false,
	        string name = "",
	        EnumChannelStatus? status = null,
	        EnumChannelSearchOrderBy orderBy = EnumChannelSearchOrderBy.OrderById)
	    {
            var condition = new ChannelSearchCondition
            {
                IsDescending = isDescending,
                Page = page,
                PageCount = pageCount,
                Name = name,
                Status = status,
                OrderBy = orderBy
            };
	        var count = _ChannelService.GetChannelCount(condition);
            return PageHelper.toJson(new{TotalCount=count,Condition =condition});
	    }

        [EnableCors("*", "*", "*", SupportsCredentials = true)]
		public bool Post(ChannelModel model)
		{
		    //var parent = _ChannelService.GetChannelById(model.Parent.Id);
			var entity = new ChannelEntity
			{

				Name = model.Name,

				Status = model.Status,

				//Parent = parent,

				Adduser = (UserBase)_workContext.CurrentUser,

				Addtime = DateTime.Now,

                Upduser = (UserBase)_workContext.CurrentUser,

                Updtime = DateTime.Now,

			};
			if(_ChannelService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

        [HttpPost]
		public bool Put([FromBody]ChannelModel model)
		{
			var entity = _ChannelService.GetChannelById(model.Id);
			if(entity == null)
				return false;

			entity.Name = model.Name;

			entity.Status = model.Status;

//			entity.Parent = model.Parent;

			entity.Upduser = (UserBase)_workContext.CurrentUser;

			entity.Updtime = DateTime.Now;

			if(_ChannelService.Update(entity) != null)
				return true;
			return false;
		}

        [HttpGet]
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