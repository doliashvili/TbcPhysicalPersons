using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tbc.PhysicalPersonsDirectory.Application.Exceptions;
using Tbc.PhysicalPersonsDirectory.Application.Extensions.CustomMappers;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Update.Model;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;
using Tbc.PhysicalPersonsDirectory.Domain.Repositories;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Update
{
    public class UpdatePhysicalPersonCommandHandler : IRequestHandler<UpdatePhysicalPersonCommand, UpdatePhysicalPersonResponse>
    {
        private readonly IRepository<PhysicalPersonEntity> _personRepository;
        private readonly IRepository<PhoneNumberEntity> _phoneRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdatePhysicalPersonCommandHandler> _logger;

        public UpdatePhysicalPersonCommandHandler(IRepository<PhysicalPersonEntity> personRepository,
            IRepository<PhoneNumberEntity> phoneRepository,
            IUnitOfWork unitOfWork,
            ILogger<UpdatePhysicalPersonCommandHandler> logger)
        {
            _personRepository = personRepository;
            _phoneRepository = phoneRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UpdatePhysicalPersonResponse> Handle(UpdatePhysicalPersonCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start update physical person with Id: '{Id}'", request.Id);

            var phoneNumbers = request.PhoneNumbers.ConvertAll(r => r.Number);

            // We can do this and also do it like in CreatePhysicalPerson with our dapper
            var isPhoneExist = await _phoneRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(x => phoneNumbers.Contains(x.Number)) // Use the list for filtering
                .AnyAsync(cancellationToken);

            if (isPhoneExist)
            {
                throw new ConflictException("Phone already exist");
            }

            var isPersonalNumberExist = await _personRepository
                .GetQueryable()
                .AsNoTracking()
                .AnyAsync(x =>
                        x.PersonalNumber == request.PersonalNumber,
                    cancellationToken);

            if (isPersonalNumberExist)
            {
                throw new ConflictException("PersonalNumber already exist");
            }

            var personEntity = await _personRepository.GetByIdAsync(request.Id);

            if (personEntity is null)
            {
                throw new ObjectNotFoundException($"Physical person not found with this id: {request.Id}");
            }

            request.MapUpdateToPhysicalPersonEntity(personEntity);

            await _personRepository.UpdateAsync(personEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Physical person was updated with Id: '{Id}'", personEntity.Id);

            return new UpdatePhysicalPersonResponse { Success = true };
        }
    }
}