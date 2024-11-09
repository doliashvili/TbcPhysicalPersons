using MediatR;
using Microsoft.Extensions.Logging;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetReport.Model;
using Tbc.PhysicalPersonsDirectory.Application.Services;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetReport
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, GetReportQueryResponse>
    {
        private readonly IReportService _reportService;
        private readonly ILogger<GetReportQueryHandler> _logger;

        public GetReportQueryHandler(IReportService reportService, ILogger<GetReportQueryHandler> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        public async Task<GetReportQueryResponse> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start generate report");

            var reports = await _reportService.GetPhysicalPersonsReportAsync();

            return new GetReportQueryResponse { Reports = reports };
        }
    }
}