using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.Extensions;

public static class EntityConfigurationExtension
{
    public static EntityTypeBuilder SetPropertyCommums(this EntityTypeBuilder builder)
    {
        builder.Property("CreatedAt")
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp(6)")
            .IsRequired();

        builder.Property("CreatedBy")
            .HasColumnName("CreatedBy")
            .HasColumnType("bigint")
            .IsRequired();

    builder.Property("UpdatedAt")
        .HasColumnName("UpdatedAt")
        .HasColumnType("timestamp(6)")
        .HasDefaultValue(null);

        builder.Property("UpdatedBy")
            .HasColumnName("UpdatedBy")
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property("DeletedAt")
                .HasColumnName("DeletedAt")
                .HasColumnType("timestamp(6)")
                .HasDefaultValue(null);
        
        return builder;
    }
}
