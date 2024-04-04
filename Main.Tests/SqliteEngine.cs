using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace Main.Tests;

public class SqliteEngine: IDbEngine
{
    public DbConnection OpenConnection()
    {
        return new SqliteConnection("Data Source=file:test.db?mode=memory&cache=shared");
    }

    public async Task InitializeAsync() {}

    public async Task DisposeAsync() {}
}