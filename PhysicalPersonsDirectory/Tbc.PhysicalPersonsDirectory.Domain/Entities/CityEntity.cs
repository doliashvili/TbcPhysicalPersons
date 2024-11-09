using Tbc.PhysicalPersonsDirectory.Domain.Abstracts;

namespace Tbc.PhysicalPersonsDirectory.Domain.Entities;

public sealed class CityEntity : Entity<int>
{
    public string Name { get; set; }
}