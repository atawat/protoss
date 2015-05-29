using System.Linq;
using Protoss.Entity.Model;
using YooPoon.Core.Autofac;

namespace Protoss.Service.Banner
{
	public interface IBannerService : IDependency
	{
		BannerEntity Create (BannerEntity entity);

		bool Delete(BannerEntity entity);

		BannerEntity Update (BannerEntity entity);

		BannerEntity GetBannerById (int id);

		IQueryable<BannerEntity> GetBannersByCondition(BannerSearchCondition condition);

		int GetBannerCount (BannerSearchCondition condition);
	}
}