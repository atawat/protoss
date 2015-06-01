using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Entity.Model;
using Protoss.Service.Category;
using YooPoon.Core.Site;
using YooPoon.WebFramework.API;
using Protoss.Models;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Controllers
{
    [AllowAnonymous]
    [EnableCors("*", "*", "*", SupportsCredentials = true)]
	public class CategoryController : ApiController
	{
		private readonly ICategoryService _CategoryService;
        private readonly IWorkContext _workContext;

        public CategoryController(ICategoryService CategoryService,IWorkContext workContext)
		{
			_CategoryService = CategoryService;
		    _workContext = workContext;
		}

		public CategoryModel Get(int id)
		{
			var entity =_CategoryService.GetCategoryById(id);
		    var model = new CategoryModel
		    {

		        Id = entity.Id,

		        CategoryName = entity.CategoryName,

//		        Father = entity.Father,

		        Adduser = new UserModel{Id = entity.Adduser.Id,UserName = entity.Adduser.UserName},

		        Addtime = entity.Addtime,

                Upduser = new UserModel { Id = entity.Upduser.Id, UserName = entity.Upduser.UserName },

		        Updtime = entity.Updtime,

//		        Products = entity.Products,

		    };
			return model;
		}

		public List<CategoryModel> Get()
		{
			var model = _CategoryService.GetCategorysByCondition(new CategorySearchCondition()).Select(c=>new CategoryModel
			{

				Id = c.Id,

				CategoryName = c.CategoryName,

//				Father = c.Father,

                Adduser = new UserModel { Id = c.Adduser.Id, UserName = c.Adduser.UserName },

				Addtime = c.Addtime,

                Upduser = new UserModel { Id = c.Upduser.Id, UserName = c.Upduser.UserName },

				Updtime = c.Updtime,

//				Products = c.Products,

			}).ToList();
			return model;
		}

		public bool Post(CategoryModel model)
		{
			var entity = new CategoryEntity
			{

				CategoryName = model.CategoryName,

//				Father = model.Father,

				Adduser = (UserBase)_workContext.CurrentUser,

				Addtime = DateTime.Now,

				Upduser = (UserBase)_workContext.CurrentUser,

                Updtime = DateTime.Now,

//				Products = model.Products,

			};
			if(_CategoryService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

        [HttpPost]
		public bool Put(CategoryModel model)
		{
			var entity = _CategoryService.GetCategoryById(model.Id);
			if(entity == null)
				return false;

			entity.CategoryName = model.CategoryName;

//			entity.Father = model.Father;
//
//			entity.Adduser = model.Adduser;
//
//			entity.Addtime = model.Addtime;

            entity.Upduser = (UserBase)_workContext.CurrentUser;

			entity.Updtime = DateTime.Now;

//			entity.Products = model.Products;

			if(_CategoryService.Update(entity) != null)
				return true;
			return false;
		}

        [HttpGet]
		public bool Delete(int id)
		{
			var entity = _CategoryService.GetCategoryById(id);
			if(entity == null)
				return false;
			if(_CategoryService.Delete(entity))
				return true;
			return false;
		}
	}
}