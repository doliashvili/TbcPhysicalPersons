using Tbc.PhysicalPersonsDirectory.Application.Models;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetDetailedFilteredPagedData.Model
{
    public sealed class GetDetailedFilteredPagedDataQueryResponse
    {
        public List<PhysicalPersonDto> PhysicalPersons { get; set; }
    }
}