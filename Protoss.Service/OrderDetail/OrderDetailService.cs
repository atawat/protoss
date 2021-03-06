using System;
using System.Linq;
using YooPoon.Core.Data;
using YooPoon.Core.Logging;
using Protoss.Entity.Model;

namespace Protoss.Service.OrderDetail
{
	public class OrderDetailService : IOrderDetailService
	{
		private readonly IRepository<OrderDetailEntity> _orderdetailRepository;
		private readonly ILog _log;

		public OrderDetailService(IRepository<OrderDetailEntity> orderdetailRepository,ILog log)
		{
			_orderdetailRepository = orderdetailRepository;
			_log = log;
		}
		
		public OrderDetailEntity Create (OrderDetailEntity entity)
		{
			try
            {
                _orderdetailRepository.Insert(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public bool Delete(OrderDetailEntity entity)
		{
			try
            {
                _orderdetailRepository.Delete(entity);
                return true;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return false;
            }
		}

		public OrderDetailEntity Update (OrderDetailEntity entity)
		{
			try
            {
                _orderdetailRepository.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public OrderDetailEntity GetOrderDetailById (int id)
		{
			try
            {
                return _orderdetailRepository.GetById(id);
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

        public IQueryable<OrderDetailEntity> GetOrderDetailByOrderId(int orderId)
        {
            var query = _orderdetailRepository.Table;
            try
            {
                query = query.Where(q => q.Order.Id==orderId);
                return query.OrderBy(q=>q.Id);
            }
            catch (Exception e)
            {
                _log.Error(e, "数据库操作出错");
                return null;
            }
        }

		public IQueryable<OrderDetailEntity> GetOrderDetailsByCondition(OrderDetailSearchCondition condition)
		{
			var query = _orderdetailRepository.Table;
			try
			{
				if (condition.Ids != null && condition.Ids.Any())
                {
                    query = query.Where(q => condition.Ids.Contains(q.Id));
                }
				if(condition.OrderBy.HasValue)
				{
					switch (condition.OrderBy.Value)
                    {
						case EnumOrderDetailSearchOrderBy.OrderById:
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

		public int GetOrderDetailCount (OrderDetailSearchCondition condition)
		{
			var query = _orderdetailRepository.Table;
			try
			{
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