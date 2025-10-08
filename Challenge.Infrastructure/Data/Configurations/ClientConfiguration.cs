using Challenge.Domain.Entities;
using Challenge.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.Data.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Client");

        builder.HasKey(c => c.Id).HasName("ClientId");

        builder.Property(c => c.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(c => c.Name).HasColumnName("Name").IsRequired();
        builder.Property(c => c.Email).HasColumnName("Email").IsRequired();
        builder.Property(c => c.Phone).HasColumnName("Phone").IsRequired();

        builder.HasIndex(c => c.Email).IsUnique();

        builder.HasOne(c => c.User).WithOne(u => u.Client)
            .HasForeignKey<Client>(c => c.UserId).OnDelete(DeleteBehavior.Cascade);

        builder.SetPropertyCommums();
    }
}
