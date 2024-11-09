using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tbc.PhysicalPersonsDirectory.Application.Exceptions;
using Tbc.PhysicalPersonsDirectory.Application.Extensions.CustomMappers;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetByIdIncludedData.Model;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;
using Tbc.PhysicalPersonsDirectory.Domain.Repositories;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetByIdIncludedData
{
    public class GetByIdIncludedDataQueryHandler : IRequestHandler<GetByIdIncludedDataQuery, GetByIdIncludedDataResponse>
    {
        private readonly IRepository<PhysicalPersonEntity> _repository;
        private readonly ILogger<GetByIdIncludedDataQueryHandler> _logger;

        public GetByIdIncludedDataQueryHandler(IRepository<PhysicalPersonEntity> repository,
            ILogger<GetByIdIncludedDataQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<GetByIdIncludedDataResponse> Handle(GetByIdIncludedDataQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start getting full data physical person with Id: '{Id}'", request.PhysicalPersonId);

            var personEntity = await _repository.GetByIdIncludedDataAsync(
                request.PhysicalPersonId,
                query => query
                    .Include(x => x.PhoneNumbers)
                    .Include(x => x.RelatedPersons)
                    .ThenInclude(rp => rp.RelatedEntity)
                    .ThenInclude(rp => rp.PhoneNumbers));

            if (personEntity is null)
            {
                throw new ObjectNotFoundException($"Physical person not found with this id: {request.PhysicalPersonId}");
            }

            var physicalPersonDto = personEntity.MapToGetByIdIncludedDataResponse();

            return physicalPersonDto;
        }
    }
}