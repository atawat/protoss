using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;
using YooPoon.WebFramework.User.Entity;


namespace Protoss.Models
{
	public class OrderModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// 订单号
        /// </summary>
		public string OrderNum {get;set;}


		/// <summary>
        /// 订单合计总价
        /// </summary>
		public decimal TotalPrice {get;set;}


		/// <summary>
        /// 运费
        /// </summary>
		public decimal TransCost {get;set;}


		/// <summary>
        /// 商品总价
        /// </summary>
		public decimal ProductCost {get;set;}


		/// <summary>
        /// 折扣
        /// </summary>
		public decimal Discount  {get;set;}


		/// <summary>
        /// 状态
        /// </summary>
		public EnumOrderStatus Status {get;set;}

		public string StatusString
		{
			get
			{
				switch(Status)
				{

					case EnumOrderStatus.Created:
						return "新建的";

					case EnumOrderStatus.Payed:
						return "已付款";

					case EnumOrderStatus.Delivering:
						return "送货中";

					case EnumOrderStatus.Confirmed:
						return "已收货";

					case EnumOrderStatus.Canceled:
						return "已取消";

					case EnumOrderStatus.Finished:
						return "已完成";

					default:
						return "";
				}
			}
		}


		/// <summary>
        /// 收获地址
        /// </summary>
		public string DeliveryAddress {get;set;}


		/// <summary>
        /// 是否打印
        /// </summary>
		public bool IsPrint {get;set;}


		/// <summary>
        /// 收货人电话
        /// </summary>
		public string PhoneNumber {get;set;}


		/// <summary>
        /// 添加人
        /// </summary>
		public UserModel Adduser {get;set;}


		/// <summary>
        /// 添加时间
        /// </summary>
		public DateTime Addtime {get;set;}


		/// <summary>
        /// 更新人
        /// </summary>
		public UserModel Upduser {get;set;}


		/// <summary>
        /// 更新时间
        /// </summary>
		public DateTime Updtime {get;set;}


		/// <summary>
        /// 订单明细
        /// </summary>
        public IList<OrderDetailModel> Details { get; set; }


		/// <summary>
        /// 订单所使用优惠卷
        /// </summary>
        public IList<CouponModel> Coupon { get; set; }


		/// <summary>
        /// 订单类型
        /// </summary>
		public EnumOrderType Type {get;set;}

		public string TypeString
		{
			get
			{
				switch(Type)
				{

					case EnumOrderType.OnLine:
						return "线上";

					case EnumOrderType.OffLine:
						return "线下";

					default:
						return "";
				}
			}
		}


		/// <summary>
        /// 支付类型
        /// </summary>
		public EnumPayType PayType {get;set;}

		public string PayTypeString
		{
			get
			{
				switch(PayType)
				{

					case EnumPayType.Cash:
						return "现金";

					case EnumPayType.WeiPay:
						return "微信支付";

					case EnumPayType.Coupon:
						return "代金卷";

					case EnumPayType.Fixed:
						return "混合";

					default:
						return "";
				}
			}
		}


		/// <summary>
        /// 收货地址坐标X
        /// </summary>
		public decimal LocationX {get;set;}


		/// <summary>
        /// 收获地址坐标Y
        /// </summary>
		public decimal LocationY {get;set;}



	}
}