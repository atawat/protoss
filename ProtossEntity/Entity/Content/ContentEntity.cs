using System;
using System.Collections.Generic;
using YooPoon.Core.Data;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Entity.Model
{
	public class ContentEntity : IBaseEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		public virtual int Id { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		public virtual string Content { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		public virtual string Title { get; set; }
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
		/// 状态
		/// </summary>
		public virtual EnumContentStatus Status { get; set; }
		/// <summary>
		/// 点赞数
		/// </summary>
		public virtual int Praise { get; set; }
		/// <summary>
		/// 点踩数
		/// </summary>
		public virtual int Unpraise { get; set; }
		/// <summary>
		/// 点击数
		/// </summary>
		public virtual int Viewcount { get; set; }
		/// <summary>
		/// 标签
		/// </summary>
		public virtual IList<TagEntity> Tags { get; set; }
		/// <summary>
		/// 所属频道
		/// </summary>
		public virtual IList<ChannelEntity> Channels { get; set; }
	}
}