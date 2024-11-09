using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Domain.Entities;

public sealed class RelatedPerson
{
    public int PhysicalPersonEntityId { get; set; }  // The ID of the main entity
    public int RelatedEntityId { get; set; }         // The ID of the related entity (another PhysicalPersonEntity)

    public RelationshipType Relationship { get; set; }

    // Navigation properties
    public PhysicalPersonEntity PhysicalPersonEntity { get; set; }

    public PhysicalPersonEntity RelatedEntity { get; set; }
}