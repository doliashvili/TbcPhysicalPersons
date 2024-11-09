using Tbc.PhysicalPersonsDirectory.Application.Models;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetByIdIncludedData.Model
{
    public sealed class GetByIdIncludedDataResponse
    {
        public PhysicalPersonDto PhysicalPerson { get; set; }

        public List<RelationPhysicalPersonDto> RelationPersons { get; set; }
    }
}