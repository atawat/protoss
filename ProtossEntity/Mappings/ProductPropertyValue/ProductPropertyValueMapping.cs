using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.ProductPropertyValue
{
	public class ProductPropertyValueMapping : EntityTypeConfiguration<ProductPropertyValueEntity>, IMapping
	{
		public ProductPropertyValueMapping()
		{
			ToTable("ProductPropertyValue");
			HasKey(c => c.Id);
			Property(c => c.Id).HasColumnType("int");
			HasOptional(c =>c.Property);
			HasOptional(c =>c.PropertyValue);
			HasOptional(c =>c.Product);
			HasRequired(c => c.Adduser);
			Property(c => c.Addtime).HasColumnType("datetime");
			HasOptional(c => c.UpdUser);
			Property(c => c.UpdTime).HasColumnType("datetime").IsOptional();
		}
	}
}