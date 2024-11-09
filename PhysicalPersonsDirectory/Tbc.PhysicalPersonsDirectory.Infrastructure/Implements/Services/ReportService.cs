using Dapper;
using Microsoft.Data.SqlClient;
using Tbc.PhysicalPersonsDirectory.Application.Models;
using Tbc.PhysicalPersonsDirectory.Application.Services;
using Tbc.PhysicalPersonsDirectory.Infrastructure.Models;
using Tbc.PhysicalPersonsDirectory.Persistence.CommonSql;

namespace Tbc.PhysicalPersonsDirectory.Infrastructure.Implements.Services
{
    public class ReportService : IReportService
    {
        private readonly DatabaseConnectionString _databaseConnectionString;

        public ReportService(DatabaseConnectionString databaseConnectionString)
        {
            _databaseConnectionString = databaseConnectionString;
        }

        public async Task<List<PersonRelationshipReport>> GetPhysicalPersonsReportAsync()
        {
            const string sql = @"
                    SELECT
                        p.Id AS PersonId,
                        CONCAT(p.FirstName, ' ', p.LastName) AS FullName,
                        rp.Relationship AS RelationshipType,
                        COUNT(rp.RelatedEntityId) AS RelatedPersonsCount
                     FROM
                         PhysicalPersons p WITH (NOLOCK)
                     LEFT JOIN
                     RelatedPersons rp WITH (NOLOCK) ON rp.PhysicalPersonEntityId = p.Id
                     GROUP BY
                     p.Id, p.FirstName, p.LastName, rp.Relationship
                     ORDER BY
                     p.Id;";

            await using var connection = new SqlConnection(_databaseConnectionString.Value);
            await connection.EnsureIsOpenAsync();

            var reportData = await connection.QueryAsync<PersonRelationshipReportDto>(sql);

            // Grouping data by PersonId to shape it as required
            var result = reportData
                .GroupBy(r => new { r.PersonId, r.FullName })
                .Select(group => new PersonRelationshipReport
                {
                    PersonId = group.Key.PersonId,
                    FullName = group.Key.FullName,
                    Relationships = group.Select(g => new RelationshipReport
                    {
                        RelationshipType = g.RelationshipType,
                        RelatedPersonsCount = g.RelatedPersonsCount
                    }).AsList()
                })
                .AsList();

            return result;
        }
    }
}