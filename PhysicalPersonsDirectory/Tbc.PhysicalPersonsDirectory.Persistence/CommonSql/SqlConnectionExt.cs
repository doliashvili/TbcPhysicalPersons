using Microsoft.Data.SqlClient;
using System.Data;

namespace Tbc.PhysicalPersonsDirectory.Persistence.CommonSql;

public static class SqlConnectionExt
{
    public static Task EnsureIsOpenAsync(this SqlConnection self, CancellationToken cancellationToken = default) =>
        self.State != ConnectionState.Open ? self.OpenAsync(cancellationToken) : Task.CompletedTask;
}