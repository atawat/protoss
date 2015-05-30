







using System;

namespace Protoss.Entity.Model
{
	public class OrderSearchCondition
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





		public string OrderNum { get; set; }







		public EnumOrderStatus? Status { get; set; }





		public string DeliveryAddress { get; set; }



		public bool? IsPrint { get; set; }





		public string PhoneNumber { get; set; }







		public EnumOrderType? Type { get; set; }





		public EnumPayType? PayType { get; set; }





		public decimal? LocationX { get; set; }



		public decimal? LocationY { get; set; }

        public DateTime? AddTimeBegin { get; set; }

        public DateTime? AddTimeEnd { get; set; }




		public EnumOrderSearchOrderBy? OrderBy { get; set; }

	}


	public enum EnumOrderSearchOrderBy
	{

		OrderById,

		OrderByOrderNum,

		OrderByTotalPrice,

		OrderByStatus,

		OrderByIsPrint,

		OrderByAddtime,

		OrderByUpdtime,

		OrderByType,

		OrderByPayType,

	}

}