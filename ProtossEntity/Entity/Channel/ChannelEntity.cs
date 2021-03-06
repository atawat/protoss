using System;
using System.Collections.Generic;
using YooPoon.Core.Data;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Entity.Model
{
	public class ChannelEntity : IBaseEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		public virtual int Id { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public virtual EnumChannelStatus Status { get; set; }
		/// <summary>
		/// 父级
		/// </summary>
		public virtual ChannelEntity Parent { get; set; }
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

        public virtual List<ContentEntity> Contents { get; set; } 
	}
}