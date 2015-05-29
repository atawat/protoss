using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;

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
		public Category Father {get;set;}


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
        /// 关联产品
        /// </summary>
		public IList<Prdocut> Products {get;set;}



	}
}