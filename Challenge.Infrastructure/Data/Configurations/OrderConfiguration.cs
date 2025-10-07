using Challenge.Domain.Entities;
using Challenge.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(o => o.Id).HasName("OrderId");

        builder.Property(o => o.ClientId).HasColumnName("ClientId").IsRequired();
        builder.Property(o => o.Name).HasColumnName("Name").IsRequired();
        builder.Property(o => o.Status).HasColumnName("Status").IsRequired();
        builder.Property(o => o.TotalAmount).HasColumnName("TotalAmount").IsRequired();

        builder.HasOne(o => o.Client).WithMany(c => c.Order)
            .HasForeignKey(o => o.ClientId).OnDelete(DeleteBehavior.Cascade);

        builder.SetPropertyCommums();
    }
}
