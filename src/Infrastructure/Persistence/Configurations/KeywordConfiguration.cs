using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class KeywordConfiguration : IEntityTypeConfiguration<Keyword>
{
    public void Configure(EntityTypeBuilder<Keyword> builder)
    {
        builder.Property(k => k.Value)
            .HasMaxLength(KeywordConstants.MaxLength)
            .IsRequired();

        builder.Property(k => k.StartingSeed)
            .HasMaxLength(KeywordConstants.MaxLength)
            .HasDefaultValue("");

        builder.Property(k => k.Culture)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(k => k.Ranking)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(k => k.Timestamp)
            .HasConversion(
                k => k.Ticks,
                k => new DateTime(k)
                )
            .IsRequired();
    }
}