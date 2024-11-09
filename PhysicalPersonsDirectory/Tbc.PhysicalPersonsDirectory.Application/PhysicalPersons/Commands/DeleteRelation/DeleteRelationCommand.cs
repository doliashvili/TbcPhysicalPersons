using MediatR;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.DeleteRelation.Model;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.DeleteRelation
{
    public class DeleteRelationCommand : IRequest<DeleteRelationResponse>
    {
        public int MainPersonId { get; set; }
        public int RelationPersonId { get; set; }
    }
}