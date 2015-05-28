using System.Linq;
using Protoss.Entity.Model;
using YooPoon.Core.Autofac;

namespace Protoss.Service.ProductPropertyValue
{
	public interface IProductPropertyValueService : IDependency
	{
		ProductPropertyValueEntity Create (ProductPropertyValueEntity entity);

		bool Delete(ProductPropertyValueEntity entity);

		ProductPropertyValueEntity Update (ProductPropertyValueEntity entity);

		ProductPropertyValueEntity GetProductPropertyValueById (int id);

		IQueryable<ProductPropertyValueEntity> GetProductPropertyValuesByCondition(ProductPropertyValueSearchCondition condition);

		int GetProductPropertyValueCount (ProductPropertyValueSearchCondition condition);
	}
}