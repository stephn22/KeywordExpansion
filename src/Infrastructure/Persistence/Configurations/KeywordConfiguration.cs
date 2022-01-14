using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class KeywordConfiguration : IEntityTypeConfiguration<Keyword>
{
    public void Configure(EntityTypeBuilder<Keyword> builder)
    {
        builder.Property(k => k.Value)
            .HasMaxLength(8000)
            .IsRequired();

        builder.HasIndex(k => k.Value)
            .IsUnique();

        builder.Property(k => k.Culture)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(k => k.Ranking)
            .IsRequired();
    }
}