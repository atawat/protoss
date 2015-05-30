using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.Coupon
{
	public class CouponMapping : EntityTypeConfiguration<CouponEntity>, IMapping
	{
		public CouponMapping()
		{
			ToTable("Coupon");
			HasKey(c => c.Id);
			//Property(c => c.Id).HasColumnType("int");
			Property(c => c.Guid).HasColumnType("uniqueidentifier");
			Property(c => c.Type).HasColumnType("int");
			Property(c => c.DisCount).HasColumnType("decimal").IsOptional();
			HasOptional(c =>c.Product);
			Property(c => c.ExpireTime).HasColumnType("datetime");
			Property(c => c.Status).HasColumnType("int");
			HasRequired(c => c.Adduser);
			Property(c => c.Addtime).HasColumnType("datetime");
			HasOptional(c => c.Upduser);
			Property(c => c.Updtime).HasColumnType("datetime").IsOptional();
			HasOptional(c => c.Owner);
		}
	}
}