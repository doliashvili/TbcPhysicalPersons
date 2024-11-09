using MediatR;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Delete.Model;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Delete
{
    public class DeletePhysicalPersonCommand : IRequest<DeletePhysicalPersonResponse>
    {
        public int Id { get; set; }
    }
}