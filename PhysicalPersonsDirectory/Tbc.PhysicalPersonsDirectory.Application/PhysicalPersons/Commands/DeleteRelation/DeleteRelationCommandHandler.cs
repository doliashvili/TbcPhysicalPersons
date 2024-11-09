using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tbc.PhysicalPersonsDirectory.Application.Exceptions;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.DeleteRelation.Model;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;
using Tbc.PhysicalPersonsDirectory.Domain.Repositories;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.DeleteRelation
{
    public class DeleteRelationCommandHandler : IRequestHandler<DeleteRelationCommand, DeleteRelationResponse>
    {
        private readonly IRepository<PhysicalPersonEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteRelationCommandHandler> _logger;

        public DeleteRelationCommandHandler(IRepository<PhysicalPersonEntity> repository,
            IUnitOfWork unitOfWork, ILogger<DeleteRelationCommandHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<DeleteRelationResponse> Handle(DeleteRelationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start deletion relations between main personId: '{personId}' and relationId: '{relationId}'", request.MainPersonId, request.RelationPersonId);

            var mainEntity = await _repository.GetByIdIncludedDataAsync(request.MainPersonId, query => query.Include(x => x.RelatedPersons));

            if (mainEntity is null)
            {
                throw new ObjectNotFoundException($"Main Physical person not found with this id: {request.MainPersonId}");
            }

            var relatedEntity = mainEntity.RelatedPersons?.Find(x => x.RelatedEntityId == request.RelationPersonId);

            if (relatedEntity is null)
            {
                throw new ConflictException($"Main Physical person have not relation with relation id: {request.RelationPersonId}");
            }

            mainEntity.RelatedPersons.Remove(relatedEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Physical person was deleted");

            return new DeleteRelationResponse { Success = true };
        }
    }
}