using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tbc.PhysicalPersonsDirectory.Persistence.CommonSql;
using Tbc.PhysicalPersonsDirectory.Persistence.Contexts;

namespace Tbc.PhysicalPersonsDirectory.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(PhysicalPersonsContext));

            services.AddDbContext<PhysicalPersonsContext>(options =>
                options.UseSqlServer(connectionString));

            var connectionStringInstance = new DatabaseConnectionString(connectionString);
            services.AddSingleton(_ => connectionStringInstance);

            return services;
        }
    }
}