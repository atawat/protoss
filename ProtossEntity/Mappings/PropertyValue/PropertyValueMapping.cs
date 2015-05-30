using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.PropertyValue
{
	public class PropertyValueMapping : EntityTypeConfiguration<PropertyValueEntity>, IMapping
	{
		public PropertyValueMapping()
		{
			ToTable("PropertyValue");
			HasKey(c => c.Id);
			//Property(c => c.Id).HasColumnType("int");
			HasOptional(c =>c.Property);
			Property(c => c.Value).HasColumnType("varchar").HasMaxLength(20);
		    HasRequired(c => c.Adduser);
			Property(c => c.Addtime).HasColumnType("datetime");
		    HasOptional(c => c.UpdUser);
			Property(c => c.UpdTime).HasColumnType("datetime").IsOptional();
		}
	}
}