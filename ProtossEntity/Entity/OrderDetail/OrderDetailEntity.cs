using System;
using System.Collections.Generic;
using YooPoon.Core.Data;

namespace Protoss.Entity.Model
{
	public class OrderDetailEntity : IBaseEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		public virtual int Id { get; set; }
		/// <summary>
		/// 对应商品
		/// </summary>
		public virtual ProductEntity Product { get; set; }
		/// <summary>
		/// 购买数量
		/// </summary>
		public virtual decimal Count { get; set; }
		/// <summary>
		/// 总价
		/// </summary>
		public virtual decimal TotalPrice { get; set; }
		/// <summary>
		/// 对应订单主表
		/// </summary>
		public virtual OrderEntity Order { get; set; }
	}
}