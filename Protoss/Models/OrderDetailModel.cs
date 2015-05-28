using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;

namespace Protoss.Models
{
	public class OrderDetailModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// 对应商品
        /// </summary>
		public Product Product {get;set;}


		/// <summary>
        /// 购买数量
        /// </summary>
		public decimal Count {get;set;}


		/// <summary>
        /// 总价
        /// </summary>
		public decimal TotalPrice {get;set;}


		/// <summary>
        /// 对应订单主表
        /// </summary>
		public Order Order {get;set;}



	}
}