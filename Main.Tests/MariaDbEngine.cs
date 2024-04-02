using System.Data.Common;
using MySqlConnector;
using Testcontainers.MariaDb;

namespace Main.Tests;

public class MariaDbEngine: IDbEngine
{
    private readonly MariaDbContainer container = new MariaDbBuilder()
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
        return new MySqlConnection(this.container.GetConnectionString());
    }
}