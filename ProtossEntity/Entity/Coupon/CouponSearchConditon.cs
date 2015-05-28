







using System;
using YooPoon.WebFramework.User.Entity;

namespace Protoss.Entity.Model
{
	public class CouponSearchCondition
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





		public Guid Guid { get; set; }





		public EnumCouponType? Type { get; set; }





		public decimal? DisCountBegin { get; set; }

		public decimal? DisCountEnd { get; set; }



		public EnumCouponStatus? Status { get; set; }









		public UserBase Owner { get; set; }




		public EnumCouponSearchOrderBy? OrderBy { get; set; }

	}


	public enum EnumCouponSearchOrderBy
	{

		OrderById,

		OrderByGuid,

		OrderByType,

		OrderByStatus,

		OrderByAddtime,

		OrderByUpdtime,

	}

}