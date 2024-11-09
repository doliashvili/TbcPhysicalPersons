using MediatR;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetFilteredPagedData.Model;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetFilteredPagedData
{
    public class GetFilteredPagedDataQuery : IRequest<GetFilteredPagedDataQueryResponse>
    {
        public int? PageNumber { get; set; }
        public string SearchQuery { get; set; }
    }
}