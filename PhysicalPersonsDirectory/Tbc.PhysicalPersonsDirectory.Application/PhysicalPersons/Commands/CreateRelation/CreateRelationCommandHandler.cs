using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tbc.PhysicalPersonsDirectory.Application.Exceptions;
using Tbc.PhysicalPersonsDirectory.Application.Extensions.CustomMappers;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.CreateRelation.Model;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;
using Tbc.PhysicalPersonsDirectory.Domain.Repositories;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.CreateRelation
{
    public class CreateRelationCommandHandler : IRequestHandler<CreateRelationCommand, CreateRelationResponse>
    {
        private readonly IRepository<PhysicalPersonEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateRelationCommandHandler> _logger;

        public CreateRelationCommandHandler(IRepository<PhysicalPersonEntity> repository,
            IUnitOfWork unitOfWork,
            ILogger<CreateRelationCommandHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CreateRelationResponse> Handle(CreateRelationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start creating relations between main personId: '{personId}' and relationId: '{relationId}'", request.MainPersonId, request.RelationPersonId);

            var mainEntity = await _repository.GetByIdIncludedDataAsync(request.MainPersonId, query => query.Include(x => x.RelatedPersons));

            if (mainEntity is null)
            {
                throw new ObjectNotFoundException($"Main Physical person not found with this id: {request.MainPersonId}");
            }

            if (mainEntity.RelatedPersons?.Any(x => x.RelatedEntityId == request.RelationPersonId) == true)
            {
                throw new ConflictException($"Main Physical person already have relation with relation id: {request.RelationPersonId}");
            }

            var relationEntity = await _repository.GetByIdIncludedDataAsync(request.RelationPersonId, query => query.Include(x => x.RelatedPersons));

            if (relationEntity is null)
            {
                throw new ObjectNotFoundException($"Relation Physical person not found with this id: {request.RelationPersonId}");
            }

            var relation = request.MapToRelatedPerson();

            mainEntity.RelatedPersons ??= new List<RelatedPerson>();
            mainEntity.RelatedPersons.Add(relation);

            await _repository.UpdateAsync(mainEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("finish creating relations");

            return new CreateRelationResponse { Success = true };
        }
    }
}