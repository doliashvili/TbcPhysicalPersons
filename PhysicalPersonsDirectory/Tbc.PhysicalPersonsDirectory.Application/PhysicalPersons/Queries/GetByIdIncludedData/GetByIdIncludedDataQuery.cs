using MediatR;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetByIdIncludedData.Model;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetByIdIncludedData
{
    public class GetByIdIncludedDataQuery : IRequest<GetByIdIncludedDataResponse>
    {
        public int PhysicalPersonId { get; set; }
    }
}