using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;
using YooPoon.Core.Data;

namespace Protoss.Entity.Mappings
{
    public class ContengTagMapping : EntityTypeConfiguration<ContentTag>, IMapping
    {
        public ContengTagMapping()
        {
            ToTable("Content_Tag");
            HasKey(c => c.Id);
            HasRequired(c=>c.Content).WithMany(c=>c.ContentTags).WillCascadeOnDelete();
            HasRequired(c=>c.Tag).WithMany(t=>t.ContentTags).WillCascadeOnDelete();
        }
    }
}