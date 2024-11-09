using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tbc.PhysicalPersonsDirectory.Application.Extensions.CustomMappers;
using Tbc.PhysicalPersonsDirectory.Application.Options;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetFilteredPagedData.Model;
using Tbc.PhysicalPersonsDirectory.Application.Services;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetFilteredPagedData
{
    public class GetFilteredPagedDataQueryHandler : IRequestHandler<GetFilteredPagedDataQuery, GetFilteredPagedDataQueryResponse>
    {
        private readonly IPhysicalPersonSearchService _reportSearchService;
        private readonly PageOptions _pageOptions;
        private readonly ILogger<GetFilteredPagedDataQueryHandler> _logger;

        public GetFilteredPagedDataQueryHandler(IPhysicalPersonSearchService reportSearchService,
            IOptionsMonitor<PageOptions> options,
            ILogger<GetFilteredPagedDataQueryHandler> logger)
        {
            _reportSearchService = reportSearchService;
            _pageOptions = options.CurrentValue;
            _logger = logger;
        }

        public async Task<GetFilteredPagedDataQueryResponse> Handle(GetFilteredPagedDataQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start filter physical person by query: {query}", request.SearchQuery);

            var pageNumber = request.PageNumber ?? 1;
            var pageSize = _pageOptions.PageSize;

            var physicalPersonEntities = await _reportSearchService.SearchPagedAsync(pageNumber, pageSize, request.SearchQuery);
            var physicalPersonDtos = physicalPersonEntities.ConvertAll(x => x.MapToPhysicalPersonDtoResponse());

            return new GetFilteredPagedDataQueryResponse { PhysicalPersons = physicalPersonDtos };
        }
    }
}