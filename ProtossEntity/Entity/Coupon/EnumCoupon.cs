
namespace Protoss.Entity.Model
{

	public enum EnumCouponType
	{
		/// <summary>
		/// 折扣卷
		/// </summary>
		Discount,
		/// <summary>
		/// 抵价卷
		/// </summary>
		Convert,
		/// <summary>
		/// 免额卷 
		/// </summary>
		Free,
	}
}

namespace Protoss.Entity.Model
{

	public enum EnumCouponStatus
	{
		/// <summary>
		/// 新建
		/// </summary>
		Created,
		/// <summary>
		/// 正常
		/// </summary>
		Normal,
		/// <summary>
		/// 已使用
		/// </summary>
		Consumed,
		/// <summary>
		/// 过期的
		/// </summary>
		Expired,
	}
}
