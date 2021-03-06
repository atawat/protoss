using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;
using YooPoon.WebFramework.User.Entity;


namespace Protoss.Models
{
	public class ProductPropertyValueModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// 属性
        /// </summary>
		public string PropertyName {get;set;}

        public int PropertyId { get; set; }

//        public int[] PropertyValueId { get; set; }
//
//
//		/// <summary>
//        /// 属性值
//        /// </summary>
//		public string[] PropertyValue {get;set;}

        public List<PropertyValueModel> PropertyValues { get; set; } 


		/// <summary>
        /// 商品
        /// </summary>
		public int ProductId {get;set;}


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



	}
}