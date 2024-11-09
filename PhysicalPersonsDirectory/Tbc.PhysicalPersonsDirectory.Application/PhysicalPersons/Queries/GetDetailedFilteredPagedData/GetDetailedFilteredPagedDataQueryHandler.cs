using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tbc.PhysicalPersonsDirectory.Application.Extensions.CustomMappers;
using Tbc.PhysicalPersonsDirectory.Application.Options;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetDetailedFilteredPagedData.Model;
using Tbc.PhysicalPersonsDirectory.Application.Services;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetDetailedFilteredPagedData
{
    public class GetDetailedFilteredPagedDataQueryHandler : IRequestHandler<GetDetailedFilteredPagedDataQuery, GetDetailedFilteredPagedDataQueryResponse>
    {
        private readonly IPhysicalPersonSearchService _reportSearchService;
        private readonly PageOptions _pageOptions;
        private readonly ILogger<GetDetailedFilteredPagedDataQueryHandler> _logger;

        public GetDetailedFilteredPagedDataQueryHandler(IPhysicalPersonSearchService reportSearchService,
            IOptionsMonitor<PageOptions> options,
            ILogger<GetDetailedFilteredPagedDataQueryHandler> logger)
        {
            _reportSearchService = reportSearchService;
            _pageOptions = options.CurrentValue;
            _logger = logger;
        }

        public async Task<GetDetailedFilteredPagedDataQueryResponse> Handle(GetDetailedFilteredPagedDataQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start filter detail physical person");

            var pageNumber = request.PageNumber ?? 1;
            var pageSize = _pageOptions.PageSize;

            var physicalPersonEntities = await _reportSearchService.SearchDetailedAsync(pageNumber, pageSize, request);
            var physicalPersonDtos = physicalPersonEntities.ConvertAll(x => x.MapToPhysicalPersonDtoResponse());

            return new GetDetailedFilteredPagedDataQueryResponse { PhysicalPersons = physicalPersonDtos };
        }
    }
}