using System.Linq;
using Protoss.Entity.Model;
using YooPoon.Core.Autofac;

namespace Protoss.Service.Category
{
	public interface ICategoryService : IDependency
	{
		CategoryEntity Create (CategoryEntity entity);

		bool Delete(CategoryEntity entity);

		CategoryEntity Update (CategoryEntity entity);

		CategoryEntity GetCategoryById (int id);

		IQueryable<CategoryEntity> GetCategorysByCondition(CategorySearchCondition condition);

		int GetCategoryCount (CategorySearchCondition condition);
	}
}