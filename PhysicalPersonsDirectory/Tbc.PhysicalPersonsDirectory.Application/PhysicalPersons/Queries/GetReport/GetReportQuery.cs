using MediatR;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetReport.Model;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetReport
{
    public class GetReportQuery : IRequest<GetReportQueryResponse>
    { }
}