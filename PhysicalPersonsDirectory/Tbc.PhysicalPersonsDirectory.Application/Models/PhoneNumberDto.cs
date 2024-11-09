using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Application.Models;

public sealed class PhoneNumberDto
{
    public string Number { get; set; }
    public PhoneType Type { get; set; }
}