







using System;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Entity.Model
{
	public class TagSearchCondition
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





		public string Tag { get; set; }



		public UserBase[] Addusers { get; set; }



		public DateTime? AddtimeBegin { get; set; }

		public DateTime? AddtimeEnd { get; set; }



		public UserBase[] UpdUsers { get; set; }



		public DateTime? UpdTimeBegin { get; set; }

		public DateTime? UpdTimeEnd { get; set; }




		public EnumTagSearchOrderBy? OrderBy { get; set; }

	}


	public enum EnumTagSearchOrderBy
	{

		OrderById,

	}

}