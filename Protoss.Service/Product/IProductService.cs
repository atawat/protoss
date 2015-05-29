using System.Linq;
using Protoss.Entity.Model;
using YooPoon.Core.Autofac;

namespace Protoss.Service.Product
{
	public interface IProductService : IDependency
	{
		ProductEntity Create (ProductEntity entity);

		bool Delete(ProductEntity entity);

		ProductEntity Update (ProductEntity entity);

		ProductEntity GetProductById (int id);

		IQueryable<ProductEntity> GetProductsByCondition(ProductSearchCondition condition);

		int GetProductCount (ProductSearchCondition condition);
	}
}