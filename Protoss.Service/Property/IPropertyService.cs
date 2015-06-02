using System.Linq;
using Protoss.Entity.Model;
using YooPoon.Core.Autofac;

namespace Protoss.Service.Property
{
	public interface IPropertyService : IDependency
	{
		PropertyEntity Create (PropertyEntity entity);

		bool Delete(PropertyEntity entity);

		PropertyEntity Update (PropertyEntity entity);

		PropertyEntity GetPropertyById (int id);

		IQueryable<PropertyEntity> GetPropertysByCondition(PropertySearchCondition condition);

		int GetPropertyCount (PropertySearchCondition condition);

	    IQueryable<PropertyEntity> GetPropertyByCategory(int categoryId);
	}
}