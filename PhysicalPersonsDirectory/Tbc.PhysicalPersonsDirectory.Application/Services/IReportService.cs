using Tbc.PhysicalPersonsDirectory.Application.Models;

namespace Tbc.PhysicalPersonsDirectory.Application.Services
{
    public interface IReportService
    {
        Task<List<PersonRelationshipReport>> GetPhysicalPersonsReportAsync();
    }
}