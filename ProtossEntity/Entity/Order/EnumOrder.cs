
namespace Protoss.Entity.Model
{

	public enum EnumOrderStatus
	{
		/// <summary>
		/// 新建的
		/// </summary>
		Created,
		/// <summary>
		/// 已付款
		/// </summary>
		Payed,
		/// <summary>
		/// 送货中
		/// </summary>
		Delivering,
		/// <summary>
		/// 已收货
		/// </summary>
		Confirmed,
		/// <summary>
		/// 已取消
		/// </summary>
		Canceled,
		/// <summary>
		/// 已完成
		/// </summary>
		Finished,
	}
}

namespace Protoss.Entity.Model
{

	public enum EnumOrderType
	{
		/// <summary>
		/// 线上
		/// </summary>
		OnLine,
		/// <summary>
		/// 线下
		/// </summary>
		OffLine,
	}
}

namespace Protoss.Entity.Model
{

	public enum EnumPayType
	{
		/// <summary>
		/// 现金
		/// </summary>
		Cash,
		/// <summary>
		/// 微信支付
		/// </summary>
		WeiPay,
		/// <summary>
		/// 代金卷
		/// </summary>
		Coupon,
		/// <summary>
		/// 混合
		/// </summary>
		Fixed,
	}
}
