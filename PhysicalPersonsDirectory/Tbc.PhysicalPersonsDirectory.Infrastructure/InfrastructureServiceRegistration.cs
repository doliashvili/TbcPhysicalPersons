using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tbc.PhysicalPersonsDirectory.Application.Services;
using Tbc.PhysicalPersonsDirectory.Domain.Repositories;
using Tbc.PhysicalPersonsDirectory.Infrastructure.Implements.Repositories;
using Tbc.PhysicalPersonsDirectory.Infrastructure.Implements.Services;

namespace Tbc.PhysicalPersonsDirectory.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Services
            services.AddScoped<IImageStorageService, ImageStorageService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IPhysicalPersonSearchService, PhysicalPersonSearchService>();

            //Repos
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}