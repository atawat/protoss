using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;
using YooPoon.WebFramework.User.Entity;


namespace Protoss.Models
{
	public class CouponModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// Guid
        /// </summary>
		public Guid Guid {get;set;}


		/// <summary>
        /// 类型
        /// </summary>
		public EnumCouponType Type {get;set;}

		public string TypeString
		{
			get
			{
				switch(Type)
				{

					case EnumCouponType.Discount:
						return "折扣卷";

					case EnumCouponType.Convert:
						return "抵价卷";

					case EnumCouponType.Free:
						return "免额卷 ";

					default:
						return "";
				}
			}
		}


		/// <summary>
        /// 折扣/优惠金额
        /// </summary>
		public decimal DisCount {get;set;}


		/// <summary>
        /// 指定产品
        /// </summary>
        public ProductModel Product { get; set; }


		/// <summary>
        /// 过期时间
        /// </summary>
		public DateTime ExpireTime {get;set;}


		/// <summary>
        /// 优惠卷状态
        /// </summary>
		public EnumCouponStatus Status {get;set;}

		public string StatusString
		{
			get
			{
				switch(Status)
				{

					case EnumCouponStatus.Created:
						return "新建";

					case EnumCouponStatus.Normal:
						return "正常";

					case EnumCouponStatus.Consumed:
						return "已使用";

					case EnumCouponStatus.Expired:
						return "过期的";

					default:
						return "";
				}
			}
		}


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
        /// 所有者
        /// </summary>
		public UserBase Owner {get;set;}



	}
}