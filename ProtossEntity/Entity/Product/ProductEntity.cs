using System;
using System.Collections.Generic;
using YooPoon.Core.Data;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Entity.Model
{
	public class ProductEntity : IBaseEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		public virtual int Id { get; set; }
		/// <summary>
		/// 商品名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 规格
		/// </summary>
		public virtual string Spec { get; set; }
		/// <summary>
		/// 价格
		/// </summary>
		public virtual decimal Price { get; set; }

        /// <summary>
        /// 主图
        /// </summary>
        public virtual string Image { get; set; }
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
		/// 单位
		/// </summary>
		public virtual string Unit { get; set; }
		/// <summary>
		/// 明细
		/// </summary>
		public virtual ProductDetailEntity Detail { get; set; }
		/// <summary>
		/// 分类
		/// </summary>
		public virtual CategoryEntity Category { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public virtual EnumProductStatus Status { get; set; }
		/// <summary>
		/// 属性值
		/// </summary>
		public virtual IList<ProductPropertyValueEntity> PropertyValues { get; set; }
	}
}