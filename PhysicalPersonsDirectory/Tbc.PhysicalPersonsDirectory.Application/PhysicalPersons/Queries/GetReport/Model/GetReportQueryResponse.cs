using Tbc.PhysicalPersonsDirectory.Application.Models;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetReport.Model
{
    public sealed class GetReportQueryResponse
    {
        public List<PersonRelationshipReport> Reports { get; set; }
    }
}