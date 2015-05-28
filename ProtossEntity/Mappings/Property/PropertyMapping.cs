using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.Property
{
	public class PropertyMapping : EntityTypeConfiguration<PropertyEntity>, IMapping
	{
		public PropertyMapping()
		{
			ToTable("Property");
			HasKey(c => c.Id);
			Property(c => c.Id).HasColumnType("int");
			Property(c => c.PropertyName).HasColumnType("varchar").HasMaxLength(20);
			HasRequired(c => c.Adduser);
			Property(c => c.Addtime).HasColumnType("datetime");
			HasOptional(c => c.UpdUser);
			Property(c => c.UpdTime).HasColumnType("datetime").IsOptional();
			HasMany(c => c.Value);
		}
	}
}