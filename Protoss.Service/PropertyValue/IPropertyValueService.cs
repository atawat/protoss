using System.Linq;
using Protoss.Entity.Model;
using YooPoon.Core.Autofac;

namespace Protoss.Service.PropertyValue
{
	public interface IPropertyValueService : IDependency
	{
		PropertyValueEntity Create (PropertyValueEntity entity);

		bool Delete(PropertyValueEntity entity);

		PropertyValueEntity Update (PropertyValueEntity entity);

		PropertyValueEntity GetPropertyValueById (int id);

		IQueryable<PropertyValueEntity> GetPropertyValuesByCondition(PropertyValueSearchCondition condition);

		int GetPropertyValueCount (PropertyValueSearchCondition condition);

	    PropertyValueEntity GetOrCreatEntityWithValue(string value,int propertyId);
	}
}