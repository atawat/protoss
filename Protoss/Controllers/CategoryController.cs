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

namespace Protoss.Controllers
{
	public class CategoryController : ApiController
	{
		private readonly ICategoryService _CategoryService;

		public CategoryController(ICategoryService CategoryService)
		{
			_CategoryService = CategoryService;
		}

		public CategoryModel Get(int id)
		{
			var entity =_CategoryService.GetCategoryById(id);
			var model = new CategoryModel
			{

				Id = entity.Id

				CategoryName = entity.CategoryName

				Father = entity.Father

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				Upduser = entity.Upduser

				Updtime = entity.Updtime

				Products = entity.Products

			}
			return model;
		}

		public List<CategoryModel> Get(CategorySearchCondtion condition)
		{
			var model = _CategoryService.Get_CategorysByConditon(condition).Select(c=>new _CategoryModel
			{

				Id = entity.Id

				CategoryName = entity.CategoryName

				Father = entity.Father

				Adduser = entity.Adduser

				Addtime = entity.Addtime

				Upduser = entity.Upduser

				Updtime = entity.Updtime

				Products = entity.Products

			});
			return model;
		}

		public bool Post(CategoryModel model)
		{
			var entity = new CategoryEntity
			{

				CategoryName = model.CategoryName

				Father = model.Father

				Adduser = model.Adduser

				Addtime = model.Addtime

				Upduser = model.Upduser

				Updtime = model.Updtime

				Products = model.Products

			}
			if(_CategoryService.Create(entity).Id > 0)
			{
				return true;
			}
			return false;
		}

		public bool Put(CategoryModel model)
		{
			var entity = _CategoryService.GetCategoryById(model.Id);
			if(entity == null)
				return false;

			entity.CategoryName = model.CategoryName

			entity.Father = model.Father

			entity.Adduser = model.Adduser

			entity.Addtime = model.Addtime

			entity.Upduser = model.Upduser

			entity.Updtime = model.Updtime

			entity.Products = model.Products

			if(_CategoryService.Update(entity) != null)
				return true;
			return false;
		}

		public bool Delete(int id)
		{
			var entity = _CategoryService.GetCategoryById(id);
			if(entity == null)
				return false;
			if(_CategoryService.Delete(entity))
				return true;
			return false
		}
	}
}