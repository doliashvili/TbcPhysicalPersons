using MediatR;
using Tbc.PhysicalPersonsDirectory.Application.Models;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Update.Model;
using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Update
{
    public class UpdatePhysicalPersonCommand : IRequest<UpdatePhysicalPersonResponse>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; }
    }
}