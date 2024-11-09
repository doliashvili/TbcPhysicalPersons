using MediatR;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.CreateRelation.Model;
using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.CreateRelation
{
    public class CreateRelationCommand : IRequest<CreateRelationResponse>
    {
        public int MainPersonId { get; set; }
        public int RelationPersonId { get; set; }
        public RelationshipType RelationshipType { get; set; }
    }
}