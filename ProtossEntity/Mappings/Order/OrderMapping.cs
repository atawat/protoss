using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.Order
{
	public class OrderMapping : EntityTypeConfiguration<OrderEntity>, IMapping
	{
		public OrderMapping()
		{
			ToTable("Order");
			HasKey(c => c.Id);
			Property(c => c.Id).HasColumnType("int");
			Property(c => c.OrderNum).HasColumnType("varchar").HasMaxLength(20);
			Property(c => c.TotalPrice).HasColumnType("decimal");
			Property(c => c.TransCost).HasColumnType("decimal");
			Property(c => c.ProductCost).HasColumnType("decimal");
			Property(c => c.Discount ).HasColumnType("decimal");
			Property(c => c.Status).HasColumnType("int");
			Property(c => c.DeliveryAddress).HasColumnType("varchar");
			Property(c => c.IsPrint).HasColumnType("bit");
			Property(c => c.PhoneNumber).HasColumnType("varchar").HasMaxLength(15);
			HasRequired(c => c.Adduser);
			Property(c => c.Addtime).HasColumnType("datetime");
			HasOptional(c => c.Upduser);
			Property(c => c.Updtime).HasColumnType("datetime").IsOptional();
			HasMany(c => c.Details);
			HasMany(c => c.Coupon);
			Property(c => c.Type).HasColumnType("int");
			Property(c => c.PayType).HasColumnType("int");
			Property(c => c.LocationX).HasColumnType("decimal").IsOptional();
			Property(c => c.LocationY).HasColumnType("decimal").IsOptional();
		}
	}
}