using System;
using System.Linq;
using YooPoon.Core.Data;
using YooPoon.Core.Logging;
using Protoss.Entity.Model;

namespace Protoss.Service.Order
{
	public class OrderService : IOrderService
	{
		private readonly IRepository<OrderEntity> _orderRepository;
		private readonly ILog _log;

		public OrderService(IRepository<OrderEntity> orderRepository,ILog log)
		{
			_orderRepository = orderRepository;
			_log = log;
		}
		
		public OrderEntity Create (OrderEntity entity)
		{
			try
            {
                _orderRepository.Insert(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public bool Delete(OrderEntity entity)
		{
			try
            {
                _orderRepository.Delete(entity);
                return true;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return false;
            }
		}

		public OrderEntity Update (OrderEntity entity)
		{
			try
            {
                _orderRepository.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public OrderEntity GetOrderById (int id)
		{
			try
            {
                return _orderRepository.GetById(id);
            }
            catch (Exception e)
            {
				_log.Error(e,"数据库操作出错");
                return null;
            }
		}

		public IQueryable<OrderEntity> GetOrdersByCondition(OrderSearchCondition condition)
		{
			var query = _orderRepository.Table;
			try
			{
				if (!string.IsNullOrEmpty(condition.OrderNum))
                {
                    query = query.Where(q => q.OrderNum == condition.OrderNum);
                }
				if (condition.Status.HasValue)
                {
                    query = query.Where(q => q.Status == condition.Status.Value);
                }
				if (condition.IsPrint.HasValue)
                {
                    query = query.Where(q => q.IsPrint == condition.IsPrint.Value);
                }
				if (condition.Type.HasValue)
                {
                    query = query.Where(q => q.Type == condition.Type.Value);
                }
				if (condition.PayType.HasValue)
                {
                    query = query.Where(q => q.PayType == condition.PayType.Value);
                }
				if (condition.LocationX.HasValue)
                {
                    query = query.Where(q => q.LocationX == condition.LocationX.Value);
                }
				if (condition.LocationY.HasValue)
                {
                    query = query.Where(q => q.LocationY == condition.LocationY.Value);
                }
				if (!string.IsNullOrEmpty(condition.DeliveryAddress))
                {
                    query = query.Where(q => q.DeliveryAddress.Contains(condition.DeliveryAddress));
                }
				if (!string.IsNullOrEmpty(condition.PhoneNumber))
                {
                    query = query.Where(q => q.PhoneNumber.Contains(condition.PhoneNumber));
                }
				if (condition.Ids != null && condition.Ids.Any())
                {
                    query = query.Where(q => condition.Ids.Contains(q.Id));
                }
			    if (condition.AddTimeBegin.HasValue)
			    {
			        query = query.Where(q => q.Addtime >= condition.AddTimeBegin);
			    }
			    if (condition.AddTimeEnd.HasValue)
			    {
			        query = query.Where(q => q.Addtime < condition.AddTimeEnd);
			    }
				if(condition.OrderBy.HasValue)
				{
					switch (condition.OrderBy.Value)
                    {
						case EnumOrderSearchOrderBy.OrderById:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Id):query.OrderBy(q=>q.Id);
							break;
						case EnumOrderSearchOrderBy.OrderByOrderNum:
							query = condition.IsDescending?query.OrderByDescending(q=>q.OrderNum):query.OrderBy(q=>q.OrderNum);
							break;
						case EnumOrderSearchOrderBy.OrderByTotalPrice:
							query = condition.IsDescending?query.OrderByDescending(q=>q.TotalPrice):query.OrderBy(q=>q.TotalPrice);
							break;
						case EnumOrderSearchOrderBy.OrderByStatus:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Status):query.OrderBy(q=>q.Status);
							break;
						case EnumOrderSearchOrderBy.OrderByIsPrint:
							query = condition.IsDescending?query.OrderByDescending(q=>q.IsPrint):query.OrderBy(q=>q.IsPrint);
							break;
						case EnumOrderSearchOrderBy.OrderByAddtime:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Addtime):query.OrderBy(q=>q.Addtime);
							break;
						case EnumOrderSearchOrderBy.OrderByUpdtime:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Updtime):query.OrderBy(q=>q.Updtime);
							break;
						case EnumOrderSearchOrderBy.OrderByType:
							query = condition.IsDescending?query.OrderByDescending(q=>q.Type):query.OrderBy(q=>q.Type);
							break;
						case EnumOrderSearchOrderBy.OrderByPayType:
							query = condition.IsDescending?query.OrderByDescending(q=>q.PayType):query.OrderBy(q=>q.PayType);
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

		public int GetOrderCount (OrderSearchCondition condition)
		{
			var query = _orderRepository.Table;
			try
			{
				if (!string.IsNullOrEmpty(condition.OrderNum))
                {
                    query = query.Where(q => q.OrderNum == condition.OrderNum);
                }
				if (condition.Status.HasValue)
                {
                    query = query.Where(q => q.Status == condition.Status.Value);
                }
				if (condition.IsPrint.HasValue)
                {
                    query = query.Where(q => q.IsPrint == condition.IsPrint.Value);
                }
				if (condition.Type.HasValue)
                {
                    query = query.Where(q => q.Type == condition.Type.Value);
                }
				if (condition.PayType.HasValue)
                {
                    query = query.Where(q => q.PayType == condition.PayType.Value);
                }
				if (condition.LocationX.HasValue)
                {
                    query = query.Where(q => q.LocationX == condition.LocationX.Value);
                }
				if (condition.LocationY.HasValue)
                {
                    query = query.Where(q => q.LocationY == condition.LocationY.Value);
                }
				if (!string.IsNullOrEmpty(condition.DeliveryAddress))
                {
                    query = query.Where(q => q.DeliveryAddress.Contains(condition.DeliveryAddress));
                }
				if (!string.IsNullOrEmpty(condition.PhoneNumber))
                {
                    query = query.Where(q => q.PhoneNumber.Contains(condition.PhoneNumber));
                }
				if (condition.Ids != null && condition.Ids.Any())
                {
                    query = query.Where(q => condition.Ids.Contains(q.Id));
                }
                if (condition.AddTimeBegin.HasValue)
                {
                    query = query.Where(q => q.Addtime >= condition.AddTimeBegin);
                }
                if (condition.AddTimeEnd.HasValue)
                {
                    query = query.Where(q => q.Addtime < condition.AddTimeEnd);
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