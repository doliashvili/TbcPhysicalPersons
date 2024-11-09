using MediatR;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetDetailedFilteredPagedData.Model;
using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetDetailedFilteredPagedData
{
    public class GetDetailedFilteredPagedDataQuery : IRequest<GetDetailedFilteredPagedDataQueryResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
        public Gender? Gender { get; set; }
        public int? PageNumber { get; set; }
    }
}