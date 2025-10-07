using Challenge.Domain.Entities;
using Challenge.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");

        builder.HasKey(oi => oi.Id).HasName("OrderItemId");

        builder.Property(oi => oi.OrderId).HasColumnName("OrderId").IsRequired();
        builder.Property(oi => oi.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(oi => oi.Quantity).HasColumnName("Quantity").IsRequired();
        builder.Property(oi => oi.IndividualAmount).HasColumnName("IndividualAmount").HasColumnType("Numeric(15,2)").IsRequired();

        builder.HasOne(oi => oi.Order).WithMany(o => o.OrderItems).HasForeignKey(oi => oi.OrderId);
        builder.HasOne(oi => oi.Product).WithMany(p => p.OrderItems).HasForeignKey(oi => oi.ProductId);

        builder.HasIndex(oi => new { oi.OrderId, oi.ProductId }).IsUnique();

        builder.SetPropertyCommums();
    }
}
