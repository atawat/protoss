using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;
using YooPoon.WebFramework.User.Entity;


namespace Protoss.Models
{
	public class PropertyModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// 属性
        /// </summary>
		public string PropertyName {get;set;}


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
		public UserModel UpdUser {get;set;}


		/// <summary>
        /// 更新时间
        /// </summary>
		public DateTime UpdTime {get;set;}


		/// <summary>
        /// 拥有的值
        /// </summary>
        public IList<PropertyValueModel> Value { get; set; }



	}
}