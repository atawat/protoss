using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.Content
{
    public class ContentMapping : EntityTypeConfiguration<ContentEntity>, IMapping
    {
        public ContentMapping()
        {
            ToTable("Content");
            HasKey(c => c.Id);
            //Property(c => c.Id).HasColumnType("int");
            Property(c => c.Content).HasColumnType("varchar").IsOptional();
            Property(c => c.Title).HasColumnType("varchar").HasMaxLength(50);
            HasRequired(c => c.Adduser);
            Property(c => c.Addtime).HasColumnType("datetime");
            HasOptional(c => c.Upduser);
            Property(c => c.Updtime).HasColumnType("datetime").IsOptional();
            Property(c => c.Status).HasColumnType("int");
            Property(c => c.Praise).HasColumnType("int");
            Property(c => c.Unpraise).HasColumnType("int");
            Property(c => c.Viewcount).HasColumnType("int");
            HasOptional(c => c.Channel);
        }
    }
}