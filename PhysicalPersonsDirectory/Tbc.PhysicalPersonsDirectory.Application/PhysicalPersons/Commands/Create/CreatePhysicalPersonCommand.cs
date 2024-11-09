using MediatR;
using Tbc.PhysicalPersonsDirectory.Application.Models;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Create.Model;
using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Create
{
    public class CreatePhysicalPersonCommand : IRequest<CreatePhysicalPersonResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; }
    }
}