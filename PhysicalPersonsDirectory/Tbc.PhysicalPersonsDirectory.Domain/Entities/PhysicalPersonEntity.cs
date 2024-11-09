using Tbc.PhysicalPersonsDirectory.Domain.Abstracts;
using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Domain.Entities
{
    public sealed class PhysicalPersonEntity : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public string PicturePath { get; set; }

        // one to many
        public List<PhoneNumberEntity> PhoneNumbers { get; set; }

        // Self-referencing many-to-many relationship with a payload
        public List<RelatedPerson> RelatedPersons { get; set; }
    }
}