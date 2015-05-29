using System;
using System.Linq;
using YooPoon.Core.Data;
using YooPoon.Core.Logging;
using Protoss.Entity.Model;

namespace Protoss.Service.Member
{
	public class MemberService : IMemberService
	{
		private readonly IRepository<MemberEntity> _memberRepository;
		private readonly ILog _log;

		public MemberService(IRepository<MemberEntity> memberRepository,ILog log)
		{
			_memberRepository = memberRepository;
			_log = log;
		}
		
		public MemberEntity Create (MemberEntity entity)
		{
			try
            {
                _memberRepository.Insert(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public bool Delete(MemberEntity entity)
		{
			try
            {
                _memberRepository.Delete(entity);
                return true;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return false;
            }
		}

		public MemberEntity Update (MemberEntity entity)
		{
			try
            {
                _memberRepository.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public MemberEntity GetMemberById (int id)
		{
			try
            {
                return _memberRepository.GetById(id);
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public IQueryable<MemberEntity> GetMembersByCondition(MemberSearchCondition condition)
		{
			var query = _memberRepository.Table;
			try
			{
				if (!string.IsNullOrEmpty(condition.OpenId))
                {
                    query = query.Where(q => q.OpenId == condition.OpenId);
                }
				if (!string.IsNullOrEmpty(condition.ContactName))
                {
                    query = query.Where(q => q.ContactName.Contains(condition.ContactName));
                }
				if (condition.Ids != null && condition.Ids.Any())
                {
                    query = query.Where(q => condition.Ids.Contains(q.Id));
                }
				if(condition.OrderBy.HasValue)
				{
					switch (condition.OrderBy.Value)
                    {
						case EnumMemberSearchOrderBy.OrderById:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Id):query.OrderBy(q=>q.Id);
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

		public int GetMemberCount (MemberSearchCondition condition)
		{
			var query = _memberRepository.Table;
			try
			{
				if (!string.IsNullOrEmpty(condition.OpenId))
                {
                    query = query.Where(q => q.OpenId == condition.OpenId);
                }
				if (!string.IsNullOrEmpty(condition.ContactName))
                {
                    query = query.Where(q => q.ContactName.Contains(condition.ContactName));
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