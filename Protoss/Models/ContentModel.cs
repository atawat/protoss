using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;
using YooPoon.WebFramework.User.Entity;


namespace Protoss.Models
{
	public class ContentModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// 内容
        /// </summary>
		public string Content {get;set;}


		/// <summary>
        /// 标题
        /// </summary>
		public string Title {get;set;}


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
        /// 状态
        /// </summary>
		public EnumContentStatus Status {get;set;}

		public string StatusString
		{
			get
			{
				switch(Status)
				{

					case EnumContentStatus.Created:
						return "刚新建";

					case EnumContentStatus.Published:
						return "已发布";

					case EnumContentStatus.Deleted:
						return "已删除";

					default:
						return "";
				}
			}
		}


		/// <summary>
        /// 点赞数
        /// </summary>
		public int Praise {get;set;}


		/// <summary>
        /// 点踩数
        /// </summary>
		public int Unpraise {get;set;}


		/// <summary>
        /// 点击数
        /// </summary>
		public int Viewcount {get;set;}


		/// <summary>
        /// 标签
        /// </summary>
        public IList<TagModel> Tags { get; set; }


		/// <summary>
        /// 所属频道
        /// </summary>
        public IList<ChannelModel> Channels { get; set; }



	}
}