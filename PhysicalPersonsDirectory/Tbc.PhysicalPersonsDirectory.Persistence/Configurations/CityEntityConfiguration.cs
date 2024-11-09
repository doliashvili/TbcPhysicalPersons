using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;
using Tbc.PhysicalPersonsDirectory.Persistence.Seeds;

namespace Tbc.PhysicalPersonsDirectory.Persistence.Configurations;

public class CityEntityConfiguration : IEntityTypeConfiguration<CityEntity>
{
    public void Configure(EntityTypeBuilder<CityEntity> builder)
    {
        // Primary key - auto-generated, unique, positive integer
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(50);

        // Seed data for Georgian cities
        builder.HasData(SeedsForTesting.Cities);
    }
}