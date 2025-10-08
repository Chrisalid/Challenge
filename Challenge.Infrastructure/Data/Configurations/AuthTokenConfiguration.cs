using Challenge.Domain.Entities;
using Challenge.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.Data.Configurations;

public class AuthTokenConfiguration : IEntityTypeConfiguration<AuthToken>
{
    public void Configure(EntityTypeBuilder<AuthToken> builder)
    {
        builder.ToTable("AuthToken");

        builder.HasKey(at => at.Id).HasName("AuthTokenId");

        builder.Property(at => at.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(at => at.Token).HasColumnName("Token").IsRequired();
        builder.Property(at => at.ExpiresAt).HasColumnName("ExpiresAt").IsRequired();

        builder.HasOne(at => at.User).WithMany(u => u.AuthToken)
            .HasForeignKey(at => at.UserId).OnDelete(DeleteBehavior.Cascade);

        builder.SetPropertyCommums();
    }
}
