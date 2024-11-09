using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;

namespace Tbc.PhysicalPersonsDirectory.Persistence.Configurations;

public class PhoneNumberEntityConfiguration : IEntityTypeConfiguration<PhoneNumberEntity>
{
    public void Configure(EntityTypeBuilder<PhoneNumberEntity> builder)
    {
        // Primary key - auto-generated, unique, positive integer
        builder.HasKey(x => x.Id);

        // Configure Number with min and max length constraints
        builder
            .Property(x => x.Number)
            .IsRequired()
            .HasMaxLength(50); // Ensures database side constraint up to 50 chars

        builder.HasIndex(x => x.Number).IsUnique();
    }
}