using System;
using System.Linq;
using YooPoon.Core.Data;
using YooPoon.Core.Logging;
using Protoss.Entity.Model;

namespace Protoss.Service.Category
{
	public class CategoryService : ICategoryService
	{
		private readonly IRepository<CategoryEntity> _categoryRepository;
		private readonly ILog _log;

		public CategoryService(IRepository<CategoryEntity> categoryRepository,ILog log)
		{
			_categoryRepository = categoryRepository;
			_log = log;
		}
		
		public CategoryEntity Create (CategoryEntity entity)
		{
			try
            {
                _categoryRepository.Insert(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public bool Delete(CategoryEntity entity)
		{
			try
            {
                _categoryRepository.Delete(entity);
                return true;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return false;
            }
		}

		public CategoryEntity Update (CategoryEntity entity)
		{
			try
            {
                _categoryRepository.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public CategoryEntity GetCategoryById (int id)
		{
			try
            {
                return _categoryRepository.GetById(id);
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public IQueryable<CategoryEntity> GetCategorysByCondition(CategorySearchCondition condition)
		{
			var query = _categoryRepository.Table;
			try
			{
				if (condition.Father !=null)
				{
				    query = query.Where(q => q.Father == condition.Father);
				}
				if (condition.Ids != null && condition.Ids.Any())
                {
                    query = query.Where(q => condition.Ids.Contains(q.Id));
                }
				if(condition.OrderBy.HasValue)
				{
					switch (condition.OrderBy.Value)
                    {
						case EnumCategorySearchOrderBy.OrderById:
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

		public int GetCategoryCount (CategorySearchCondition condition)
		{
			var query = _categoryRepository.Table;
			try
			{
				if (condition.Father != null)
                {
                    query = query.Where(q => q.Father == condition.Father);
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