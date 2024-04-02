using System.Data.Common;
using MySqlConnector;
using Npgsql;

namespace Main.User;

public interface IConnectionFactory
{
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