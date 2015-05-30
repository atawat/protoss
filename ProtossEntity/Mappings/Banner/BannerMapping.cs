using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.Banner
{
	public class BannerMapping : EntityTypeConfiguration<BannerEntity>, IMapping
	{
		public BannerMapping()
		{
			ToTable("Banner");
			HasKey(c => c.Id);
//			//Property(c => c.Id).HasColumnType("int");
			Property(c => c.Title).HasColumnType("varchar").HasMaxLength(50);
			Property(c => c.ImgUrl).HasColumnType("varchar").HasMaxLength(200);
			Property(c => c.Order).HasColumnType("int");
			HasRequired(c => c.Adduser);
			Property(c => c.Addtime).HasColumnType("datetime");
			HasOptional(c => c.Upduser);
			Property(c => c.Updtime).HasColumnType("datetime").IsOptional();
			HasOptional(c =>c.Content);
		}
	}
}