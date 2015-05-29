using System.Linq;
using Protoss.Entity.Model;
using YooPoon.Core.Autofac;

namespace Protoss.Service.ProductDetail
{
	public interface IProductDetailService : IDependency
	{
		ProductDetailEntity Create (ProductDetailEntity entity);

		bool Delete(ProductDetailEntity entity);

		ProductDetailEntity Update (ProductDetailEntity entity);

		ProductDetailEntity GetProductDetailById (int id);

		IQueryable<ProductDetailEntity> GetProductDetailsByCondition(ProductDetailSearchCondition condition);

		int GetProductDetailCount (ProductDetailSearchCondition condition);
	}
}