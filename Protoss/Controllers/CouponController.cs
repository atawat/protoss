using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.Coupon;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;

namespace Protoss.Controllers
{
	public class CouponController : ApiController
	{
		private readonly ICouponService _CouponService;

		public CouponController(ICouponService CouponService)
		{
			_CouponService = CouponService;
		}

		public CouponModel Get(int id)
		{
			var entity =_CouponService.GetCouponById(id);
		    var model = new CouponModel
		    {

		        Id = entity.Id,

		        Guid = entity.Guid,

		        Type = entity.Type,

		        DisCount = entity.DisCount,

//		        Product = entity.Product,

		        ExpireTime = entity.ExpireTime,

		        Status = entity.Status,

		        Adduser = entity.Adduser,

		        Addtime = entity.Addtime,

		        Upduser = entity.Upduser,

		        Updtime = entity.Updtime,

		        Owner = entity.Owner,

		    };
			return model;
		}

		public List<CouponModel> Get(CouponSearchCondition condition)
		{
			var model = _CouponService.GetCouponsByCondition(condition).Select(c=>new CouponModel
			{

				Id = c.Id,

				Guid = c.Guid,

				Type = c.Type,

				DisCount = c.DisCount,

//				Product = c.Product,

				ExpireTime = c.ExpireTime,

				Status = c.Status,

				Adduser = c.Adduser,

				Addtime = c.Addtime,

				Upduser = c.Upduser,

				Updtime = c.Updtime,

				Owner = c.Owner,

			}).ToList();
			return model;
		}

		public bool Post(CouponModel model)
		{
			var entity = new CouponEntity
			{

				Guid = model.Guid,

				Type = model.Type,

				DisCount = model.DisCount,

//				Product = model.Product,

				ExpireTime = model.ExpireTime,

				Status = model.Status,

				Adduser = model.Adduser,

				Addtime = model.Addtime,

				Upduser = model.Upduser,

				Updtime = model.Updtime,

				Owner = model.Owner,

			};
			if(_CouponService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(CouponModel model)
		{
			var entity = _CouponService.GetCouponById(model.Id);
			if(entity == null)
				return false;

			entity.Guid = model.Guid;

			entity.Type = model.Type;

			entity.DisCount = model.DisCount;

//			entity.Product = model.Product;

			entity.ExpireTime = model.ExpireTime;

			entity.Status = model.Status;

			entity.Adduser = model.Adduser;

			entity.Addtime = model.Addtime;

			entity.Upduser = model.Upduser;

			entity.Updtime = model.Updtime;

			entity.Owner = model.Owner;

			if(_CouponService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _CouponService.GetCouponById(id);
			if(entity == null)
				return false;
			if(_CouponService.Delete(entity))
				return true;
			return false;
		}
	}
}