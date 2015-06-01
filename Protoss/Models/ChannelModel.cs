using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;
using YooPoon.WebFramework.User.Entity;


namespace Protoss.Models
{
	public class ChannelModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// 名称
        /// </summary>
		public string Name {get;set;}


		/// <summary>
        /// 状态
        /// </summary>
		public EnumChannelStatus Status {get;set;}

		public string StatusString
		{
			get
			{
				switch(Status)
				{

					case EnumChannelStatus.Normal:
						return "正常";

					case EnumChannelStatus.Deleted:
						return "已删除";

					default:
						return "";
				}
			}
		}


		/// <summary>
        /// 父级
        /// </summary>
        public ChannelModel Parent { get; set; }


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
        public UserModel Upduser { get; set; }


		/// <summary>
        /// 更新时间
        /// </summary>
		public DateTime Updtime {get;set;}



	}
}