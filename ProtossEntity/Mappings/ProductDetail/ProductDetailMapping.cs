using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.ProductDetail
{
	public class ProductDetailMapping : EntityTypeConfiguration<ProductDetailEntity>, IMapping
	{
		public ProductDetailMapping()
		{
			ToTable("ProductDetail");
			HasKey(c => c.Id);
			//Property(c => c.Id).HasColumnType("int");
			Property(c => c.Detail).HasColumnType("varchar").IsOptional();
			Property(c => c.ImgUrl1).HasColumnType("varchar").HasMaxLength(300).IsOptional();
			Property(c => c.ImgUrl2).HasColumnType("varchar").HasMaxLength(300).IsOptional();
			Property(c => c.ImgUrl3).HasColumnType("varchar").HasMaxLength(300).IsOptional();
			Property(c => c.ImgUrl4).HasColumnType("varchar").HasMaxLength(300).IsOptional();
			Property(c => c.ImgUrl5).HasColumnType("varchar").HasMaxLength(300).IsOptional();
			HasRequired(c =>c.Product);
		}
	}
}