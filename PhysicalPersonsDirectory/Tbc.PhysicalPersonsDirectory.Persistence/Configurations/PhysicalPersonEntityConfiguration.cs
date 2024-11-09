using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;

namespace Tbc.PhysicalPersonsDirectory.Persistence.Configurations;

public class PhysicalPersonEntityConfiguration : IEntityTypeConfiguration<PhysicalPersonEntity>
{
    public void Configure(EntityTypeBuilder<PhysicalPersonEntity> builder)
    {
        // Primary key - auto-generated, unique, positive integer
        builder.HasKey(x => x.Id);

        // FirstName - required, between 2 and 50 characters, only Georgian or Latin letters, but not both
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50)
            .HasAnnotation("MinLength", 2)
            .HasConversion(
                v => v,
                v => v.Trim())
            .IsUnicode()
            .HasAnnotation("RegularExpression", "^(?:[ა-ჰ]+|[A-Za-z]+)$");

        // LastName - required, between 2 and 50 characters, only Georgian or Latin letters, but not both
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50)
            .HasAnnotation("MinLength", 2)
            .HasConversion(
                v => v,
                v => v.Trim())
            .IsUnicode()
            .HasAnnotation("RegularExpression", "^(?:[ა-ჰ]+|[A-Za-z]+)$");

        // Gender - required, only "Female" or "Male" values
        builder.Property(x => x.Gender)
            .IsRequired();

        // PersonalNumber - required, exactly 11 digits
        builder.Property(x => x.PersonalNumber)
            .IsRequired()
            .HasMaxLength(11)
            .IsFixedLength()
            .HasAnnotation("RegularExpression", @"^\d{11}$");

        // BirthDate - required, must be at least 18 years old
        builder.Property(x => x.BirthDate)
            .IsRequired()
            .HasConversion(
                v => v,
                v => v.Date) // Ensure only date is stored
            .HasAnnotation("MinAge", 18);

        // CityId - required (foreign key from city reference table)
        builder.Property(x => x.CityId)
            .IsRequired();

        // PicturePath - optional, relative file path
        builder.Property(x => x.PicturePath)
            .IsRequired(false)
            .HasMaxLength(255);

        // Relationship with City - one PhysicalPersonEntity has one CityEntity
        builder.HasOne<CityEntity>() // CityEntity is the principal entity
            .WithMany() // One City can have many people
            .HasForeignKey(x => x.CityId); // CityId is the foreign key in PhysicalPersonEntity

        // Self-referencing relationship using RelatedPerson as the join entity
        builder
            .HasMany(p => p.RelatedPersons)
            .WithOne(rp => rp.PhysicalPersonEntity)
            .HasForeignKey(rp => rp.PhysicalPersonEntityId)
            .OnDelete(DeleteBehavior.Restrict);  // Optional, adjust as needed

        builder
            .HasMany<RelatedPerson>()
            .WithOne(rp => rp.RelatedEntity)
            .HasForeignKey(rp => rp.RelatedEntityId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevents cascade delete to avoid cyclic deletion

        // Configure one-to-many relationship between PhysicalPersonEntity and PhoneNumberEntity
        builder
            .HasMany(p => p.PhoneNumbers) // Configure the collection navigation property
            .WithOne() // No back-reference in PhoneNumberEntity
            .HasForeignKey(p => p.PhysicalPersonEntityId) // Foreign key in PhoneNumberEntity
            .OnDelete(DeleteBehavior.Cascade);

        // Adding index on PersonalNumber for faster lookup
        builder.HasIndex(x => x.PersonalNumber).IsUnique();

        //This will be a frequent search, so let's index it
        builder.HasIndex(x => x.FirstName);

        // This will be a frequent search, so let's index it
        builder.HasIndex(x => x.LastName);
    }
}