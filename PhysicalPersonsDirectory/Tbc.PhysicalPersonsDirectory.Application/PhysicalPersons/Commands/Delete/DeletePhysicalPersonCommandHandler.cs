using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tbc.PhysicalPersonsDirectory.Application.Exceptions;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Delete.Model;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;
using Tbc.PhysicalPersonsDirectory.Domain.Repositories;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Delete
{
    public class DeletePhysicalPersonCommandHandler : IRequestHandler<DeletePhysicalPersonCommand, DeletePhysicalPersonResponse>
    {
        private readonly IRepository<PhysicalPersonEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeletePhysicalPersonCommandHandler> _logger;

        public DeletePhysicalPersonCommandHandler(IRepository<PhysicalPersonEntity> repository,
            IUnitOfWork unitOfWork, ILogger<DeletePhysicalPersonCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeletePhysicalPersonResponse> Handle(DeletePhysicalPersonCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start deleting physical person with Id: '{Id}'", request.Id);

            var personEntity = await _repository.GetByIdIncludedDataAsync(request.Id, query => query.Include(x => x.PhoneNumbers));

            if (personEntity is null)
            {
                throw new ObjectNotFoundException($"Physical person not found with this id: {request.Id}");
            }

            await _repository.DeleteAsync(personEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Physical person was deleted with Id");

            return new DeletePhysicalPersonResponse { Success = true };
        }
    }
}