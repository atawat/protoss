using System.Linq;
using Protoss.Entity.Model;
using YooPoon.Core.Autofac;

namespace Protoss.Service.OrderDetail
{
	public interface IOrderDetailService : IDependency
	{
		OrderDetailEntity Create (OrderDetailEntity entity);

		bool Delete(OrderDetailEntity entity);

		OrderDetailEntity Update (OrderDetailEntity entity);

		OrderDetailEntity GetOrderDetailById (int id);

		IQueryable<OrderDetailEntity> GetOrderDetailsByCondition(OrderDetailSearchCondition condition);

		int GetOrderDetailCount (OrderDetailSearchCondition condition);
	}
}