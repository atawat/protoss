using System;
using System.Linq;
using YooPoon.Core.Data;
using YooPoon.Core.Logging;
using Protoss.Entity.Model;

namespace Protoss.Service.Coupon
{
	public class CouponService : ICouponService
	{
		private readonly IRepository<CouponEntity> _couponRepository;
		private readonly ILog _log;

		public CouponService(IRepository<CouponEntity> couponRepository,ILog log)
		{
			_couponRepository = couponRepository;
			_log = log;
		}
		
		public CouponEntity Create (CouponEntity entity)
		{
			try
            {
                _couponRepository.Insert(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public bool Delete(CouponEntity entity)
		{
			try
            {
                _couponRepository.Delete(entity);
                return true;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return false;
            }
		}

		public CouponEntity Update (CouponEntity entity)
		{
			try
            {
                _couponRepository.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public CouponEntity GetCouponById (int id)
		{
			try
            {
                return _couponRepository.GetById(id);
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public IQueryable<CouponEntity> GetCouponsByCondition(CouponSearchCondition condition)
		{
			var query = _couponRepository.Table;
			try
			{
				if (condition.DisCountBegin.HasValue)
                {
                    query = query.Where(q => q.DisCount>= condition.DisCountBegin.Value);
                }
                if (condition.DisCountEnd.HasValue)
                {
                    query = query.Where(q => q.DisCount < condition.DisCountEnd.Value);
                }
				if (condition.Guid != null)
                {
                    query = query.Where(q => q.Guid == condition.Guid);
                }
				if (condition.Type.HasValue)
                {
                    query = query.Where(q => q.Type == condition.Type.Value);
                }
				if (condition.Status.HasValue)
                {
                    query = query.Where(q => q.Status == condition.Status.Value);
                }
				if (condition.Owner != null)
                {
                    query = query.Where(q => q.Owner == condition.Owner);
                }
				if (condition.Ids != null && condition.Ids.Any())
                {
                    query = query.Where(q => condition.Ids.Contains(q.Id));
                }
				if(condition.OrderBy.HasValue)
				{
					switch (condition.OrderBy.Value)
                    {
						case EnumCouponSearchOrderBy.OrderById:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Id):query.OrderBy(q=>q.Id);
							break;
						case EnumCouponSearchOrderBy.OrderByGuid:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Guid):query.OrderBy(q=>q.Guid);
							break;
						case EnumCouponSearchOrderBy.OrderByType:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Type):query.OrderBy(q=>q.Type);
							break;
						case EnumCouponSearchOrderBy.OrderByStatus:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Status):query.OrderBy(q=>q.Status);
							break;
						case EnumCouponSearchOrderBy.OrderByAddtime:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Addtime):query.OrderBy(q=>q.Addtime);
							break;
						case EnumCouponSearchOrderBy.OrderByUpdtime:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Updtime):query.OrderBy(q=>q.Updtime);
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

		public int GetCouponCount (CouponSearchCondition condition)
		{
			var query = _couponRepository.Table;
			try
			{
				if (condition.DisCountBegin.HasValue)
                {
                    query = query.Where(q => q.DisCount>= condition.DisCountBegin.Value);
                }
                if (condition.DisCountEnd.HasValue)
                {
                    query = query.Where(q => q.DisCount < condition.DisCountEnd.Value);
                }
				if (condition.Guid != null)
                {
                    query = query.Where(q => q.Guid == condition.Guid);
                }
				if (condition.Type.HasValue)
                {
                    query = query.Where(q => q.Type == condition.Type.Value);
                }
				if (condition.Status.HasValue)
                {
                    query = query.Where(q => q.Status == condition.Status.Value);
                }
				if (condition.Owner != null)
                {
                    query = query.Where(q => q.Owner == condition.Owner);
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