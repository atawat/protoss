using System;
using System.Collections.Generic;
using YooPoon.Core.Data;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Entity.Model
{
	public class CouponEntity : IBaseEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		public virtual int Id { get; set; }
		/// <summary>
		/// Guid
		/// </summary>
		public virtual Guid Guid { get; set; }
		/// <summary>
		/// 类型
		/// </summary>
		public virtual EnumCouponType Type { get; set; }
		/// <summary>
		/// 折扣/优惠金额
		/// </summary>
		public virtual decimal DisCount { get; set; }
		/// <summary>
		/// 指定产品
		/// </summary>
		public virtual ProductEntity Product { get; set; }
		/// <summary>
		/// 过期时间
		/// </summary>
		public virtual DateTime ExpireTime { get; set; }
		/// <summary>
		/// 优惠卷状态
		/// </summary>
		public virtual EnumCouponStatus Status { get; set; }
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
		/// 所有者
		/// </summary>
		public virtual UserBase Owner { get; set; }
	}
}