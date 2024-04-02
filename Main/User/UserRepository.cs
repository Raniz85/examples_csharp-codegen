using System.Data.Common;
using Dapper;

namespace Main.User;

public class UserRepository(IConnectionFactory connectionFactory)
{
    private DbConnection Connection => connectionFactory.OpenConnection();

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

    public async Task Save(User user)
    {
        await using var connection = this.Connection;
        await connection.OpenAsync();
        await connection.ExecuteAsync(@"INSERT INTO users (id, name, age) VALUES (@Id, @Name, @Age)", param: user);
    }

    public async Task<User?> Get(Guid userId)
    {
        await using var connection = this.Connection;
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<User>("SELECT id, name, age FROM users WHERE id = @Id",
            new { Id = userId });
    }
}