using System;
using System.Collections.Generic;
using YooPoon.Core.Data;

namespace Protoss.Entity.Model
{
	public class ProductDetailEntity : IBaseEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		public virtual int Id { get; set; }
		/// <summary>
		/// 详细
		/// </summary>
		public virtual string Detail { get; set; }
		/// <summary>
		/// 图片链接1
		/// </summary>
		public virtual string ImgUrl1 { get; set; }
		/// <summary>
		/// 图片链接2
		/// </summary>
		public virtual string ImgUrl2 { get; set; }
		/// <summary>
		/// 图片链接3
		/// </summary>
		public virtual string ImgUrl3 { get; set; }
		/// <summary>
		/// 图片链接4
		/// </summary>
		public virtual string ImgUrl4 { get; set; }
		/// <summary>
		/// 图片链接5
		/// </summary>
		public virtual string ImgUrl5 { get; set; }
		/// <summary>
		/// 商品主体
		/// </summary>
		public virtual ProductEntity Product { get; set; }
	}
}