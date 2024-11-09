using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;

namespace Tbc.PhysicalPersonsDirectory.Persistence.Configurations;

public class RelatedPersonConfiguration : IEntityTypeConfiguration<RelatedPerson>
{
    public void Configure(EntityTypeBuilder<RelatedPerson> builder)
    {
        builder.HasKey(rp => new { rp.PhysicalPersonEntityId, rp.RelatedEntityId });

        builder.Property(rp => rp.Relationship).IsRequired();

        builder
            .HasOne(rp => rp.PhysicalPersonEntity)
            .WithMany(p => p.RelatedPersons)
            .HasForeignKey(rp => rp.PhysicalPersonEntityId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevents cyclic cascade deletes

        builder
            .HasOne(rp => rp.RelatedEntity)
            .WithMany()
            .HasForeignKey(rp => rp.RelatedEntityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}