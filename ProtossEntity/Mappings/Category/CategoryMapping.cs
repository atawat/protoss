using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.Category
{
	public class CategoryMapping : EntityTypeConfiguration<CategoryEntity>, IMapping
	{
		public CategoryMapping()
		{
			ToTable("Category");
			HasKey(c => c.Id);
			//Property(c => c.Id).HasColumnType("int");
			Property(c => c.CategoryName).HasColumnType("varchar").HasMaxLength(40);
			HasOptional(c =>c.Father);
			HasRequired(c => c.Adduser);
			Property(c => c.Addtime).HasColumnType("datetime");
			HasOptional(c => c.Upduser);
			Property(c => c.Updtime).HasColumnType("datetime").IsOptional();
			HasMany(c => c.Products);
		}
	}
}