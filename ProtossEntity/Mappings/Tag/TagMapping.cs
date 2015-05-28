using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.Tag
{
	public class TagMapping : EntityTypeConfiguration<TagEntity>, IMapping
	{
		public TagMapping()
		{
			ToTable("Tag");
			HasKey(c => c.Id);
			Property(c => c.Id).HasColumnType("int");
			Property(c => c.Tag).HasColumnType("varchar").HasMaxLength(20);
			HasRequired(c => c.Adduser);
			Property(c => c.Addtime).HasColumnType("datetime");
			HasOptional(c => c.UpdUser);
			Property(c => c.UpdTime).HasColumnType("datetime").IsOptional();
			HasMany(c => c.Content);
		}
	}
}