using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetDetailedFilteredPagedData;
using Tbc.PhysicalPersonsDirectory.Application.Services;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;
using Tbc.PhysicalPersonsDirectory.Domain.Repositories;
using Tbc.PhysicalPersonsDirectory.Persistence.CommonSql;

namespace Tbc.PhysicalPersonsDirectory.Infrastructure.Implements.Services;

public class PhysicalPersonSearchService : IPhysicalPersonSearchService
{
    private readonly IRepository<PhysicalPersonEntity> _repository;
    private readonly DatabaseConnectionString _databaseConnectionString;

    public PhysicalPersonSearchService(IRepository<PhysicalPersonEntity> repository,
        DatabaseConnectionString databaseConnectionString)
    {
        _repository = repository;
        _databaseConnectionString = databaseConnectionString;
    }

    // სწრაფი ძებნა LIKE პრინციპით
    public async Task<List<PhysicalPersonEntity>> SearchPagedAsync(int pageNumber, int pageSize, string searchQuery = null)
    {
        var query = _repository.GetQueryable();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            // SQL LIKE გამოყენება
            query = query.Where(p =>
                EF.Functions.Like(p.FirstName, $"%{searchQuery}%") ||
                EF.Functions.Like(p.LastName, $"%{searchQuery}%") ||
                EF.Functions.Like(p.PersonalNumber, $"%{searchQuery}%"));
        }

        var result = query
            .Skip((pageNumber - 1) * pageSize) // გვერდის გამოტანა
            .Take(pageSize); // გვერდის ზომა

        var includedData = result.Include(x => x.PhoneNumbers);

        return await includedData.ToListAsync();
    }

    // დეტალური ძებნა ყველა ველის მიხედვით
    public async Task<List<PhysicalPersonEntity>> SearchDetailedAsync(int pageNumber, int pageSize, GetDetailedFilteredPagedDataQuery criteria)
    {
        var query = _repository.GetQueryable();

        if (!string.IsNullOrEmpty(criteria.FirstName))
            query = query.Where(p => EF.Functions.Like(p.FirstName, $"%{criteria.FirstName}%"));

        if (!string.IsNullOrEmpty(criteria.LastName))
            query = query.Where(p => EF.Functions.Like(p.LastName, $"%{criteria.LastName}%"));

        if (!string.IsNullOrEmpty(criteria.PersonalNumber))
            query = query.Where(p => EF.Functions.Like(p.PersonalNumber, $"%{criteria.PersonalNumber}%"));

        if (criteria.BirthDate.HasValue)
            query = query.Where(p => p.BirthDate == criteria.BirthDate.Value);

        if (criteria.CityId.HasValue)
            query = query.Where(p => p.CityId == criteria.CityId.Value);

        if (criteria.Gender.HasValue)
            query = query.Where(p => p.Gender == criteria.Gender.Value);

        var result = query
            .Skip((pageNumber - 1) * pageSize) // გვერდის გამოტანა
            .Take(pageSize);  // გვერდის ზომა

        var includedData = result.Include(x => x.PhoneNumbers);

        return await includedData.ToListAsync();
    }

    public async Task<bool> PhoneExistAsync(List<string> phoneNumbers)
    {
        const string sql = @"
        SELECT CASE WHEN EXISTS (
            SELECT 1
            FROM PhoneNumbers
            WHERE [Number] IN @PhoneNumbers
        ) THEN 1 ELSE 0 END";

        await using var connection = new SqlConnection(_databaseConnectionString.Value);
        await connection.EnsureIsOpenAsync();

        var exists = await connection.ExecuteScalarAsync<bool>(sql, new { PhoneNumbers = phoneNumbers });

        return exists;
    }

    public async Task<bool> PersonalNumberExistAsync(string personalNumber)
    {
        const string sql = @"
            SELECT CASE WHEN EXISTS (
                SELECT 1
                FROM PhysicalPersons
                WHERE PersonalNumber = @PersonalNumber
            ) THEN 1 ELSE 0 END";

        await using var connection = new SqlConnection(_databaseConnectionString.Value);
        await connection.EnsureIsOpenAsync();

        var exists = await connection.ExecuteScalarAsync<bool>(sql, new { PersonalNumber = personalNumber });

        return exists;
    }
}