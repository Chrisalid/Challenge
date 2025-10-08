using Challenge.Domain.Entities;
using Challenge.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(u => u.Id).HasName("UserId");

        builder.Property(u => u.Name).HasColumnName("Name").IsRequired();
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        builder.Property(u => u.Password).HasColumnName("Password").IsRequired();
        builder.Property(u => u.IsActive).HasColumnName("IsActive").IsRequired();
        builder.Property(u => u.Role).HasColumnName("Role").IsRequired();

        builder.HasIndex(u => u.Email).IsUnique();

        builder.SetPropertyCommums();
    }
}
