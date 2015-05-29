using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.Channel 
{
	public class ChannelMapping : EntityTypeConfiguration<ChannelEntity>, IMapping
	{
		public ChannelMapping()
		{
			ToTable("Channel ");
			HasKey(c => c.Id);
			Property(c => c.Id).HasColumnType("int");
			Property(c => c.Name).HasColumnType("varchar").HasMaxLength(30);
			Property(c => c.Status).HasColumnType("int");
			HasOptional(c =>c.Parent);
			HasRequired(c => c.Adduser);
			Property(c => c.Addtime).HasColumnType("datetime");
		    HasRequired(c => c.Upduser);
			Property(c => c.Updtime).HasColumnType("datetime").IsOptional();
		}
	}
}