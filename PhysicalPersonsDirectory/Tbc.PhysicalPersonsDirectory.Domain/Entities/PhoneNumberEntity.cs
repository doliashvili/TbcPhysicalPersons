using Tbc.PhysicalPersonsDirectory.Domain.Abstracts;
using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Domain.Entities;

public class PhoneNumberEntity : Entity<int>
{
    public string Number { get; set; }
    public PhoneType Type { get; set; }

    // Foreign key for PhysicalPersonEntity
    public int PhysicalPersonEntityId { get; set; }
}