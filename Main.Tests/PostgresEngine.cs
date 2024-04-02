using System.Data.Common;
using Npgsql;
using Testcontainers.PostgreSql;

namespace Main.Tests;

public class PostgresEngine : IDbEngine
{
    private readonly PostgreSqlContainer container = new PostgreSqlBuilder()
        .Build();
    
    public async Task InitializeAsync()
    {
        await this.container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await this.container.DisposeAsync();
    }

    public DbConnection OpenConnection()
    {
        return new NpgsqlConnection(this.container.GetConnectionString());
    }
}