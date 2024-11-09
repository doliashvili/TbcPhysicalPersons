using MediatR;
using Microsoft.Extensions.Logging;
using Tbc.PhysicalPersonsDirectory.Application.Exceptions;
using Tbc.PhysicalPersonsDirectory.Application.Extensions.CustomMappers;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Create.Model;
using Tbc.PhysicalPersonsDirectory.Application.Services;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;
using Tbc.PhysicalPersonsDirectory.Domain.Repositories;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Create
{
    public class CreatePhysicalPersonCommandHandler : IRequestHandler<CreatePhysicalPersonCommand, CreatePhysicalPersonResponse>
    {
        private readonly IRepository<PhysicalPersonEntity> _personRepository;
        private readonly IPhysicalPersonSearchService _searchService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreatePhysicalPersonCommandHandler> _logger;

        public CreatePhysicalPersonCommandHandler(IRepository<PhysicalPersonEntity> personRepository,
            IPhysicalPersonSearchService searchService,
            IUnitOfWork unitOfWork,
            ILogger<CreatePhysicalPersonCommandHandler> logger)
        {
            _personRepository = personRepository;
            _searchService = searchService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CreatePhysicalPersonResponse> Handle(CreatePhysicalPersonCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start creating physical person with PersonalNumber: '{PersonalNumber}'", request.PersonalNumber);

            var phoneNumbers = request.PhoneNumbers.ConvertAll(r => r.Number);

            var isPhoneExist = await _searchService.PhoneExistAsync(phoneNumbers);

            if (isPhoneExist)
            {
                throw new ConflictException("Phone already exist");
            }

            var isPersonalNumberExist = await _searchService.PersonalNumberExistAsync(request.PersonalNumber);

            if (isPersonalNumberExist)
            {
                throw new ConflictException("PersonalNumber already exist");
            }

            // Map and add the initial entity
            var personEntity = request.MapToPhysicalPersonEntity();

            await _personRepository.AddAsync(personEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Physical person was created with Id: '{Id}'", personEntity.Id);

            return new CreatePhysicalPersonResponse { PersonId = personEntity.Id };
        }
    }
}