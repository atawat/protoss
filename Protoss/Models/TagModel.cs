using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;
using YooPoon.WebFramework.User.Entity;


namespace Protoss.Models
{
	public class TagModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// 标签
        /// </summary>
		public string Tag {get;set;}


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
		public UserBase UpdUser {get;set;}


		/// <summary>
        /// 更新时间
        /// </summary>
		public DateTime UpdTime {get;set;}


		/// <summary>
        /// 关联内容
        /// </summary>
        public IList<ContentModel> Content { get; set; }



	}
}