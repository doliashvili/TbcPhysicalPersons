using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Infrastructure.Models;

public class PersonRelationshipReportDto
{
    public int PersonId { get; set; }
    public string FullName { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public int RelatedPersonsCount { get; set; }
}