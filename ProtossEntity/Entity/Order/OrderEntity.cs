using System;
using System.Collections.Generic;
using YooPoon.Core.Data;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Entity.Model
{
	public class OrderEntity : IBaseEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		public virtual int Id { get; set; }
		/// <summary>
		/// 订单号
		/// </summary>
		public virtual string OrderNum { get; set; }
		/// <summary>
		/// 订单合计总价
		/// </summary>
		public virtual decimal TotalPrice { get; set; }
		/// <summary>
		/// 运费
		/// </summary>
		public virtual decimal TransCost { get; set; }
		/// <summary>
		/// 商品总价
		/// </summary>
		public virtual decimal ProductCost { get; set; }
		/// <summary>
		/// 折扣
		/// </summary>
		public virtual decimal Discount  { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public virtual EnumOrderStatus Status { get; set; }
		/// <summary>
		/// 收获地址
		/// </summary>
		public virtual string DeliveryAddress { get; set; }
		/// <summary>
		/// 是否打印
		/// </summary>
		public virtual bool IsPrint { get; set; }
		/// <summary>
		/// 收货人电话
		/// </summary>
		public virtual string PhoneNumber { get; set; }
		/// <summary>
		/// 添加人
		/// </summary>
		public virtual UserBase Adduser { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>
		public virtual DateTime Addtime { get; set; }
		/// <summary>
		/// 更新人
		/// </summary>
		public virtual UserBase Upduser { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public virtual DateTime Updtime { get; set; }
		/// <summary>
		/// 订单明细
		/// </summary>
		public virtual IList<OrderDetailEntity> Details { get; set; }
		/// <summary>
		/// 订单所使用优惠卷
		/// </summary>
		public virtual IList<CouponEntity> Coupon { get; set; }
		/// <summary>
		/// 订单类型
		/// </summary>
		public virtual EnumOrderType Type { get; set; }
		/// <summary>
		/// 支付类型
		/// </summary>
		public virtual EnumPayType PayType { get; set; }
		/// <summary>
		/// 收货地址坐标X
		/// </summary>
		public virtual decimal LocationX { get; set; }
		/// <summary>
		/// 收获地址坐标Y
		/// </summary>
        public virtual decimal LocationY { get; set; }
	}
}