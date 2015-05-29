







using System;

namespace Protoss.Entity.Model
{
	public class CategorySearchCondition
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





		public CategoryEntity Father { get; set; }




		public EnumCategorySearchOrderBy? OrderBy { get; set; }

	}


	public enum EnumCategorySearchOrderBy
	{

		OrderById,

	}

}