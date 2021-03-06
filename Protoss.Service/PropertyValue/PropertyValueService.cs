using System;
using System.Linq;
using YooPoon.Core.Data;
using YooPoon.Core.Logging;
using Protoss.Entity.Model;
using YooPoon.Core.Site;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Service.PropertyValue
{
	public class PropertyValueService : IPropertyValueService
	{
		private readonly IRepository<PropertyValueEntity> _propertyvalueRepository;
		private readonly ILog _log;
	    private readonly IWorkContext _workContext;

	    public PropertyValueService(IRepository<PropertyValueEntity> propertyvalueRepository,ILog log,IWorkContext workContext)
		{
			_propertyvalueRepository = propertyvalueRepository;
			_log = log;
		    _workContext = workContext;
		}
		
		public PropertyValueEntity Create (PropertyValueEntity entity)
		{
			try
            {
                _propertyvalueRepository.Insert(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public bool Delete(PropertyValueEntity entity)
		{
			try
            {
                _propertyvalueRepository.Delete(entity);
                return true;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return false;
            }
		}

		public PropertyValueEntity Update (PropertyValueEntity entity)
		{
			try
            {
                _propertyvalueRepository.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public PropertyValueEntity GetPropertyValueById (int id)
		{
			try
            {
                return _propertyvalueRepository.GetById(id);
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public IQueryable<PropertyValueEntity> GetPropertyValuesByCondition(PropertyValueSearchCondition condition)
		{
			var query = _propertyvalueRepository.Table;
			try
			{
				if (condition.AddtimeBegin.HasValue)
                {
                    query = query.Where(q => q.Addtime>= condition.AddtimeBegin.Value);
                }
                if (condition.AddtimeEnd.HasValue)
                {
                    query = query.Where(q => q.Addtime < condition.AddtimeEnd.Value);
                }
				if (condition.UpdTimeBegin.HasValue)
                {
                    query = query.Where(q => q.UpdTime>= condition.UpdTimeBegin.Value);
                }
                if (condition.UpdTimeEnd.HasValue)
                {
                    query = query.Where(q => q.UpdTime < condition.UpdTimeEnd.Value);
                }
				if (condition.Property != null)
                {
                    query = query.Where(q => q.Property == condition.Property);
                }
				if (!string.IsNullOrEmpty(condition.Value))
                {
                    query = query.Where(q => q.Value == condition.Value);
                }
				if (condition.Ids != null && condition.Ids.Any())
                {
                    query = query.Where(q => condition.Ids.Contains(q.Id));
                }
				if (condition.Addusers != null && condition.Addusers.Any())
                {
                    query = query.Where(q => condition.Addusers.Contains(q.Adduser));
                }
				if (condition.UpdUsers != null && condition.UpdUsers.Any())
                {
                    query = query.Where(q => condition.UpdUsers.Contains(q.UpdUser));
                }
				if(condition.OrderBy.HasValue)
				{
					switch (condition.OrderBy.Value)
                    {
						case EnumPropertyValueSearchOrderBy.OrderById:
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

		public int GetPropertyValueCount (PropertyValueSearchCondition condition)
		{
			var query = _propertyvalueRepository.Table;
			try
			{
				if (condition.AddtimeBegin.HasValue)
                {
                    query = query.Where(q => q.Addtime>= condition.AddtimeBegin.Value);
                }
                if (condition.AddtimeEnd.HasValue)
                {
                    query = query.Where(q => q.Addtime < condition.AddtimeEnd.Value);
                }
				if (condition.UpdTimeBegin.HasValue)
                {
                    query = query.Where(q => q.UpdTime>= condition.UpdTimeBegin.Value);
                }
                if (condition.UpdTimeEnd.HasValue)
                {
                    query = query.Where(q => q.UpdTime < condition.UpdTimeEnd.Value);
                }
				if (condition.Property != null)
                {
                    query = query.Where(q => q.Property == condition.Property);
                }
				if (!string.IsNullOrEmpty(condition.Value))
                {
                    query = query.Where(q => q.Value == condition.Value);
                }
				if (condition.Ids != null && condition.Ids.Any())
                {
                    query = query.Where(q => condition.Ids.Contains(q.Id));
                }
				if (condition.Addusers != null && condition.Addusers.Any())
                {
                    query = query.Where(q => condition.Addusers.Contains(q.Adduser));
                }
				if (condition.UpdUsers != null && condition.UpdUsers.Any())
                {
                    query = query.Where(q => condition.UpdUsers.Contains(q.UpdUser));
                }
				return query.Count();
			}
			catch(Exception e)
			{
				_log.Error(e,"数据库操作出错");
                return -1;
			}
		}

	    public PropertyValueEntity GetOrCreatEntityWithValue(string value,PropertyEntity property)
	    {
	        try
	        {
	            var valueEntity = _propertyvalueRepository.Table.FirstOrDefault(c => c.Value == value && c.Property.Id == property.Id);
	            if (valueEntity == null)
	            {
	                valueEntity = new PropertyValueEntity
	                {
	                    Addtime = DateTime.Now,
                        Adduser = (UserBase)_workContext.CurrentUser,
                        Property = property,
                        UpdUser = (UserBase)_workContext.CurrentUser,
                        UpdTime = DateTime.Now,
                        Value = value
	                };
                    _propertyvalueRepository.Insert(valueEntity);
	            }
	            return valueEntity;
	        }
	        catch (Exception e)
	        {
                _log.Error(e, "数据库操作出错");
                return null;
	        }
	    }
	}
}