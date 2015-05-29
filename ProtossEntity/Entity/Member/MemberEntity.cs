using System;
using System.Collections.Generic;
using YooPoon.Core.Data;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Entity.Model
{
	public class MemberEntity : IBaseEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		public virtual int Id { get; set; }
		/// <summary>
		/// 关联用户
		/// </summary>
		public virtual UserBase User { get; set; }
		/// <summary>
		/// 微信openId
		/// </summary>
		public virtual string OpenId { get; set; }
		/// <summary>
		/// 会员订单
		/// </summary>
		public virtual IList<OrderEntity> Orders { get; set; }
		/// <summary>
		/// 真是姓名
		/// </summary>
		public virtual string ContactName { get; set; }
	}
}