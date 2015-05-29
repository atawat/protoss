using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;
using YooPoon.WebFramework.User.Entity;


namespace Protoss.Models
{
	public class MemberModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// 关联用户
        /// </summary>
		public UserBase User {get;set;}


		/// <summary>
        /// 微信openId
        /// </summary>
		public string OpenId {get;set;}


		/// <summary>
        /// 会员订单
        /// </summary>
        public IList<OrderModel> Orders { get; set; }


		/// <summary>
        /// 真是姓名
        /// </summary>
		public string ContactName {get;set;}



	}
}