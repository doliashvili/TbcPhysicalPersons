using Tbc.PhysicalPersonsDirectory.Domain.Enums;

namespace Tbc.PhysicalPersonsDirectory.Application.Models;

public class PhysicalPersonDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public string PersonalNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public int CityId { get; set; }
    public List<PhoneNumberDto> PhoneNumbers { get; set; }
    public string PicturePath { get; set; }
}