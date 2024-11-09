using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Application.Models;

public class RelationshipReport
{
    public RelationshipType RelationshipType { get; set; }
    public int RelatedPersonsCount { get; set; }
}