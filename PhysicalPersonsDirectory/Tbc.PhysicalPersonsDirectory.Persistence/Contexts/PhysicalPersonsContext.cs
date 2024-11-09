using Microsoft.EntityFrameworkCore;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;
using RelatedPerson = Tbc.PhysicalPersonsDirectory.Domain.Entities.RelatedPerson;

namespace Tbc.PhysicalPersonsDirectory.Persistence.Contexts
{
    public class PhysicalPersonsContext : DbContext
    {
        public DbSet<PhysicalPersonEntity> PhysicalPersons { get; set; }
        public DbSet<RelatedPerson> RelatedPersons { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<PhoneNumberEntity> PhoneNumbers { get; set; }

        public PhysicalPersonsContext(DbContextOptions<PhysicalPersonsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
                => modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhysicalPersonsContext).Assembly);
    }
}