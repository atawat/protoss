using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;
using YooPoon.WebFramework.User.Entity;


namespace Protoss.Models
{
	public class CategoryModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// 分类名称
        /// </summary>
		public string CategoryName {get;set;}


		/// <summary>
        /// 父类
        /// </summary>
        public CategoryModel Father { get; set; }


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
        /// 关联产品
        /// </summary>
        public IList<ProductModel> Products { get; set; }



	}
}