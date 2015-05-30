using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.Member
{
	public class MemberMapping : EntityTypeConfiguration<MemberEntity>, IMapping
	{
		public MemberMapping()
		{
			ToTable("Member");
			HasKey(c => c.Id);
			//Property(c => c.Id).HasColumnType("int");
			HasOptional(c =>c.User);
			Property(c => c.OpenId).HasColumnType("varchar").HasMaxLength(50).IsOptional();
			HasMany(c => c.Orders);
			Property(c => c.ContactName).HasColumnType("varchar").HasMaxLength(50).IsOptional();
		}
	}
}