using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetDetailedFilteredPagedData;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;

namespace Tbc.PhysicalPersonsDirectory.Application.Services
{
    public interface IPhysicalPersonSearchService
    {
        // სწრაფი ძებნა
        Task<List<PhysicalPersonEntity>> SearchPagedAsync(int pageNumber, int pageSize, string searchQuery = null);

        // დეტალური ძებნა
        Task<List<PhysicalPersonEntity>> SearchDetailedAsync(int pageNumber, int pageSize, GetDetailedFilteredPagedDataQuery criteria);

        Task<bool> PhoneExistAsync(List<string> phoneNumbers);

        Task<bool> PersonalNumberExistAsync(string personalNumber);
    }
}