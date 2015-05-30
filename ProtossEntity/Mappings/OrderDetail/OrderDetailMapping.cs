using YooPoon.Core.Data;
using System.Data.Entity.ModelConfiguration;
using Protoss.Entity.Model;

namespace Protoss.Entity.Mappings.OrderDetail
{
	public class OrderDetailMapping : EntityTypeConfiguration<OrderDetailEntity>, IMapping
	{
		public OrderDetailMapping()
		{
			ToTable("OrderDetail");
			HasKey(c => c.Id);
			//Property(c => c.Id).HasColumnType("int");
			HasOptional(c =>c.Product);
			Property(c => c.Count).HasColumnType("decimal");
			Property(c => c.TotalPrice).HasColumnType("decimal");
			HasRequired(c =>c.Order);
		}
	}
}