namespace Tbc.PhysicalPersonsDirectory.Application.Models;

public class PersonRelationshipReport
{
    public int PersonId { get; set; }
    public string FullName { get; set; }
    public List<RelationshipReport> Relationships { get; set; }
}