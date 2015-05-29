using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;

namespace Protoss.Models
{
	public class ProductModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// 商品名称
        /// </summary>
		public string Name {get;set;}


		/// <summary>
        /// 规格
        /// </summary>
		public string Spec {get;set;}


		/// <summary>
        /// 价格
        /// </summary>
		public decimal Price {get;set;}


		/// <summary>
        /// 添加人
        /// </summary>
		public UserBase Adduser {get;set;}


		/// <summary>
        /// 添加时间
        /// </summary>
		public DateTime Addtime {get;set;}


		/// <summary>
        /// 更新人
        /// </summary>
		public UserBase Upduser {get;set;}


		/// <summary>
        /// 更新时间
        /// </summary>
		public DateTime Updtime {get;set;}


		/// <summary>
        /// 单位
        /// </summary>
		public string Unit {get;set;}


		/// <summary>
        /// 明细
        /// </summary>
		public ProductDetail Detail {get;set;}


		/// <summary>
        /// 分类
        /// </summary>
		public Category Category {get;set;}


		/// <summary>
        /// 状态
        /// </summary>
		public EnumProductStatus Status {get;set;}

		public string StatusString
		{
			get
			{
				switch(Status)

					case EnumProductStatus.OnSale:
						return "上架";

					case EnumProductStatus.OffSale:
						return "下架";

					case EnumProductStatus.Deleted:
						return "已删除";

					default:
						return:"";
			}
		}


		/// <summary>
        /// 属性值
        /// </summary>
		public IList<PrductPropertyValueEntity> PropertyValues {get;set;}



	}
}