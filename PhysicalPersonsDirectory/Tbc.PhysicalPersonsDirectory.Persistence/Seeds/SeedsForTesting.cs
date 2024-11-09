using Tbc.PhysicalPersonsDirectory.Domain.Entities;

namespace Tbc.PhysicalPersonsDirectory.Persistence.Seeds
{
    public static class SeedsForTesting
    {
        public static List<CityEntity> Cities = new List<CityEntity>()
        {
            new CityEntity { Id = 1, Name = "თბილისი" },
            new CityEntity { Id = 2, Name = "ბათუმი" },
            new CityEntity { Id = 3, Name = "ქუთაისი" },
            new CityEntity { Id = 4, Name = "რუსთავი" },
            new CityEntity { Id = 5, Name = "ზუგდიდი" },
            new CityEntity { Id = 6, Name = "გორი" },
            new CityEntity { Id = 7, Name = "თელავი" },
            new CityEntity { Id = 8, Name = "ზესტაფონი" },
            new CityEntity { Id = 9, Name = "თერჯოლა" },
            new CityEntity { Id = 10, Name = "ახალციხე" }
        };
    }
}