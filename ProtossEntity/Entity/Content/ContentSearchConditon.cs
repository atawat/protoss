







using System;
using System.Collections.Generic;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Entity.Model
{
	public class ContentSearchCondition
	{
		/// <summary>
		/// 页码
		/// </summary>
		public int? Page { get; set; }

		/// <summary>
		/// 每页大小
		/// </summary>
		public int? PageCount { get; set; }

		/// <summary>
		/// 是否降序
		/// </summary>
		public bool IsDescending { get; set; }


		public int[] Ids { get; set; }





		public string Content { get; set; }



		public string Title { get; set; }





		public UserBase[] Addusers { get; set; }



		public DateTime? AddtimeBegin { get; set; }

		public DateTime? AddtimeEnd { get; set; }





		public UserBase[] Updusers { get; set; }



		public DateTime? UpdtimeBegin { get; set; }

		public DateTime? UpdtimeEnd { get; set; }





		public EnumContentStatus? Status { get; set; }



		public int[] Praises { get; set; }





		public int[] Unpraises { get; set; }





		public int[] Viewcounts { get; set; }





		public IList<string>[] Tagss { get; set; }



		public int? ChannelId { get; set; }




		public EnumContentSearchOrderBy? OrderBy { get; set; }

	}


	public enum EnumContentSearchOrderBy
	{

		OrderById,

		OrderByTitle,

		OrderByAddtime,

		OrderByUpdtime,

		OrderByPraise,

		OrderByUnpraise,

		OrderByViewcount,

	}

}