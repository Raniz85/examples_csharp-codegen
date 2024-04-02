using System.Data.Common;
using MySqlConnector;
using Npgsql;

namespace Main.User;

/// <summary>
/// A connection factory to open a DbConnection
/// </summary>
public interface IConnectionFactory
{
    /// <summary>
    /// Open a new connection
    /// </summary>
    /// <returns></returns>
    DbConnection OpenConnection();
}

public class PostgresConnectionFactory(string connectionString) : IConnectionFactory
{
    public DbConnection OpenConnection()
    {
        return new NpgsqlConnection(connectionString);
    }
}

public class MariaDbConnectionFactory(string connectionString) : IConnectionFactory
{
    public DbConnection OpenConnection()
    {
        return new MySqlConnection(connectionString);
    }
}