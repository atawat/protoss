







using System;

namespace Protoss.Entity.Model
{
	public class ProductSearchCondition
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





		public string Name { get; set; }



		public string Spec { get; set; }



		public decimal? PriceBegin { get; set; }

		public decimal? PriceEnd { get; set; }





		public int? CategoryId { get; set; }



		public EnumProductStatus? Status { get; set; }






		public EnumProductSearchOrderBy? OrderBy { get; set; }

	}


	public enum EnumProductSearchOrderBy
	{

		OrderById,

		OrderByPrice,

		OrderByStatus,

	}

}