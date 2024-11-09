using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tbc.PhysicalPersonsDirectory.Persistence.Contexts;

namespace Tbc.PhysicalPersonsDirectory.Persistence.Extensions
{
    public static class ExtensionsForTest
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<PhysicalPersonsContext>();

            context.Database.Migrate();
        }
    }
}