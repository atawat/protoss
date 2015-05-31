using System;
using System.Linq;
using YooPoon.Core.Data;
using YooPoon.Core.Logging;
using Protoss.Entity.Model;

namespace Protoss.Service.Product
{
	public class ProductService : IProductService
	{
		private readonly IRepository<ProductEntity> _productRepository;
		private readonly ILog _log;

		public ProductService(IRepository<ProductEntity> productRepository,ILog log)
		{
			_productRepository = productRepository;
			_log = log;
		}
		
		public ProductEntity Create (ProductEntity entity)
		{
			try
            {
                _productRepository.Insert(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public bool Delete(ProductEntity entity)
		{
			try
            {
                _productRepository.Delete(entity);
                return true;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return false;
            }
		}

		public ProductEntity Update (ProductEntity entity)
		{
			try
            {
                _productRepository.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public ProductEntity GetProductById (int id)
		{
			try
            {
                return _productRepository.GetById(id);
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public IQueryable<ProductEntity> GetProductsByCondition(ProductSearchCondition condition)
		{
			var query = _productRepository.Table;
			try
			{
				if (condition.PriceBegin.HasValue)
                {
                    query = query.Where(q => q.Price>= condition.PriceBegin.Value);
                }
                if (condition.PriceEnd.HasValue)
                {
                    query = query.Where(q => q.Price < condition.PriceEnd.Value);
                }
				if (!string.IsNullOrEmpty(condition.Spec))
                {
                    query = query.Where(q => q.Spec == condition.Spec);
                }
				if (condition.CategoryId != null)
                {
                    query = query.Where(q => q.Category.Id == condition.CategoryId);
                }
				if (condition.Status.HasValue)
                {
                    query = query.Where(q => q.Status == condition.Status.Value);
                }
				if (!string.IsNullOrEmpty(condition.Name))
                {
                    query = query.Where(q => q.Name.Contains(condition.Name));
                }
                if (condition.Ids != null && condition.Ids.Any())
                {
                    query = query.Where(q => condition.Ids.Contains(q.Id));
                }
				if(condition.OrderBy.HasValue)
				{
					switch (condition.OrderBy.Value)
                    {
						case EnumProductSearchOrderBy.OrderById:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Id):query.OrderBy(q=>q.Id);
							break;
						case EnumProductSearchOrderBy.OrderByPrice:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Price):query.OrderBy(q=>q.Price);
							break;
						case EnumProductSearchOrderBy.OrderByStatus:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Status):query.OrderBy(q=>q.Status);
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

		public int GetProductCount (ProductSearchCondition condition)
		{
			var query = _productRepository.Table;
			try
			{
				if (condition.PriceBegin.HasValue)
                {
                    query = query.Where(q => q.Price>= condition.PriceBegin.Value);
                }
                if (condition.PriceEnd.HasValue)
                {
                    query = query.Where(q => q.Price < condition.PriceEnd.Value);
                }
				if (!string.IsNullOrEmpty(condition.Spec))
                {
                    query = query.Where(q => q.Spec == condition.Spec);
                }
				if (condition.CategoryId != null)
                {
                    query = query.Where(q => q.Category.Id == condition.CategoryId);
                }
				if (condition.Status.HasValue)
                {
                    query = query.Where(q => q.Status == condition.Status.Value);
                }
				if (!string.IsNullOrEmpty(condition.Name))
                {
                    query = query.Where(q => q.Name.Contains(condition.Name));
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