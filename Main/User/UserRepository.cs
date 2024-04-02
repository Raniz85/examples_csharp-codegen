using System.Data.Common;
using Dapper;

namespace Main.User;

/// <summary>
/// A repository for storing users
/// </summary>
/// <param name="connectionFactory">Connection factory to provide database connections</param>
public class UserRepository(IConnectionFactory connectionFactory)
{
    private DbConnection Connection => connectionFactory.OpenConnection();

    /// <summary>
    /// Initialize the repository, creating any necessary tables
    /// </summary>
    public async Task Init()
    {
        await using var connection = this.Connection;
        await connection.OpenAsync();
        await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS users (
            id UUID PRIMARY KEY,
            name VARCHAR(100),
            age INT
        )");
    }

    /// <summary>
    /// Persist a user in the database
    /// </summary>
    /// <param name="user">The user to persist</param>
    public async Task Save(User user)
    {
        await using var connection = this.Connection;
        await connection.OpenAsync();
        await connection.ExecuteAsync(@"INSERT INTO users (id, name, age) VALUES (@Id, @Name, @Age)", param: user);
    }

    /// <summary>
    /// Retrieve a persisted user from the database
    /// </summary>
    /// <param name="userId">ID of the user to retrieve</param>
    /// <returns>The user, or null if user exists with that ID</returns>
    public async Task<User?> Get(Guid userId)
    {
        await using var connection = this.Connection;
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<User>("SELECT id, name, age FROM users WHERE id = @Id",
            new { Id = userId });
    }
}