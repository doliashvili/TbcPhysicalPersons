using Tbc.PhysicalPersonsDirectory.Application.Models;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetFilteredPagedData.Model
{
    public sealed class GetFilteredPagedDataQueryResponse
    {
        public List<PhysicalPersonDto> PhysicalPersons { get; set; }
    }
}