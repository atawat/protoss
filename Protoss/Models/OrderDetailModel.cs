using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;
using YooPoon.WebFramework.User.Entity;


namespace Protoss.Models
{
	public class OrderDetailModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}

        /// <summary>
        /// 主图
        /// </summary>
        public virtual string Image { get; set; }
		/// <summary>
        /// 对应商品
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 对应商品Id
        /// </summary>
        public int ProductId { get; set; }


		/// <summary>
        /// 购买数量
        /// </summary>
		public decimal Count {get;set;}


		/// <summary>
        /// 总价
        /// </summary>
		public decimal TotalPrice {get;set;}

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        public DateTime AddTime { get; set; }
	}
}