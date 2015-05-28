using System;
using System.Linq;
using YooPoon.Core.Data;
using YooPoon.Core.Logging;
using Protoss.Entity.Model;

namespace Protoss.Service.Banner
{
	public class BannerService : IBannerService
	{
		private readonly IRepository<BannerEntity> _bannerRepository;
		private readonly ILog _log;

		public BannerService(IRepository<BannerEntity> bannerRepository,ILog log)
		{
			_bannerRepository = bannerRepository;
			_log = log;
		}
		
		public BannerEntity Create (BannerEntity entity)
		{
			try
            {
                _bannerRepository.Insert(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public bool Delete(BannerEntity entity)
		{
			try
            {
                _bannerRepository.Delete(entity);
                return true;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return false;
            }
		}

		public BannerEntity Update (BannerEntity entity)
		{
			try
            {
                _bannerRepository.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public BannerEntity GetBannerById (int id)
		{
			try
            {
                return _bannerRepository.GetById(id);
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public IQueryable<BannerEntity> GetBannersByCondition(BannerSearchCondition condition)
		{
			var query = _bannerRepository.Table;
			try
			{
				if (!string.IsNullOrEmpty(condition.Title))
                {
                    query = query.Where(q => q.Title.Contains(condition.Title));
                }
				if (condition.Ids != null && condition.Ids.Any())
                {
                    query = query.Where(q => condition.Ids.Contains(q.Id));
                }
				if(condition.OrderBy.HasValue)
				{
					switch (condition.OrderBy.Value)
                    {
						case EnumBannerSearchOrderBy.OrderById:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Id):query.OrderBy(q=>q.Id);
							break;
						case EnumBannerSearchOrderBy.OrderByOrder:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Order):query.OrderBy(q=>q.Order);
							break;
                    }
					
				}
				else
				{
					query = query.OrderBy(q=>q.Id);
				}

				if (condition.Page.HasValue && condition.PageCount.HasValue)
                {
                    query = query.Skip((condition.Page.Value - 1)*condition.PageCount.Value).Take(condition.PageCount.Value);
                }
				return query;
			}
			catch(Exception e)
			{
				_log.Error(e,"数据库操作出错");
                return null;
			}
		}

		public int GetBannerCount (BannerSearchCondition condition)
		{
			var query = _bannerRepository.Table;
			try
			{
				if (!string.IsNullOrEmpty(condition.Title))
                {
                    query = query.Where(q => q.Title.Contains(condition.Title));
                }
				if (condition.Ids != null && condition.Ids.Any())
                {
                    query = query.Where(q => condition.Ids.Contains(q.Id));
                }
				return query.Count();
			}
			catch(Exception e)
			{
				_log.Error(e,"数据库操作出错");
                return -1;
			}
		}
	}
}