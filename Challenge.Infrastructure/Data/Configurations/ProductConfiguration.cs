using Challenge.Domain.Entities;
using Challenge.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(p => p.Id).HasName("ProductId");

        builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
        builder.Property(p => p.Description).HasColumnName("Description").IsRequired();
        builder.Property(p => p.Value).HasColumnName("Value").HasColumnType("Numeric(15,2)").IsRequired();
        builder.Property(p => p.Inventory).HasColumnName("Inventory").IsRequired();

        builder.SetPropertyCommums();
    }
}
