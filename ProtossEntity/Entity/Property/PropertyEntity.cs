using System;
using System.Collections.Generic;
using YooPoon.Core.Data;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Entity.Model
{
	public class PropertyEntity : IBaseEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		public virtual int Id { get; set; }
		/// <summary>
		/// 属性
		/// </summary>
		public virtual string PropertyName { get; set; }
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
		public virtual UserBase UpdUser { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public virtual DateTime UpdTime { get; set; }
		/// <summary>
		/// 拥有的值
		/// </summary>
		public virtual IList<PropertyValueEntity> Value { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>
        public virtual CategoryEntity Category { get; set; }
	}
}