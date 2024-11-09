using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Application.Models;

public sealed class RelationPhysicalPersonDto : PhysicalPersonDto
{
    public RelationshipType RelationshipType { get; set; }
}