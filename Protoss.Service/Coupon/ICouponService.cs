using System.Linq;
using Protoss.Entity.Model;
using YooPoon.Core.Autofac;

namespace Protoss.Service.Coupon
{
	public interface ICouponService : IDependency
	{
		CouponEntity Create (CouponEntity entity);

		bool Delete(CouponEntity entity);

		CouponEntity Update (CouponEntity entity);

		CouponEntity GetCouponById (int id);

		IQueryable<CouponEntity> GetCouponsByCondition(CouponSearchCondition condition);

		int GetCouponCount (CouponSearchCondition condition);
	}
}