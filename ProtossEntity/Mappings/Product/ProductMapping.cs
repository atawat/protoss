using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.Product
{
	public class ProductMapping : EntityTypeConfiguration<ProductEntity>, IMapping
	{
		public ProductMapping()
		{
			ToTable("Product");
			HasKey(c => c.Id);
			Property(c => c.Id).HasColumnType("int");
			Property(c => c.Name).HasColumnType("varchar").HasMaxLength(50);
			Property(c => c.Spec).HasColumnType("varchar").HasMaxLength(100);
			Property(c => c.Price).HasColumnType("decimal");
			HasRequired(c => c.Adduser);
			Property(c => c.Addtime).HasColumnType("datetime");
			HasOptional(c => c.Upduser);
			Property(c => c.Updtime).HasColumnType("datetime").IsOptional();
			Property(c => c.Unit).HasColumnType("varchar").HasMaxLength(10).IsOptional();
			HasOptional(c =>c.Detail);
			HasOptional(c =>c.Category);
			Property(c => c.Status).HasColumnType("int");
			HasMany(c => c.PropertyValues);
		}
	}
}