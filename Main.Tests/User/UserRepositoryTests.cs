using Codegen;
using FluentAssertions;
using Main.User;

namespace Main.Tests.User;

[DbTest]
public abstract class UserRepositoryTests<T>(T engine): IAsyncLifetime
    where T: IDbEngine
{
    private readonly UserRepository repository = new UserRepository(engine);

    public async Task InitializeAsync()
    {
        await engine.InitializeAsync();
        await repository.Init();
    }

    public async Task DisposeAsync()
    {
        await engine.DisposeAsync();
    }

    [Fact]
    public async Task ThatUserCanBeRoundTripped()
    {
        // Given a user
        var user = new Main.User.User(Guid.NewGuid(), "John Doe", 32);
        
        // When the user is stored in the repository
        await this.repository.Save(user);
        
        // Then it can be loaded again
        (await this.repository.Get(user.Id)).Should().Be(user);
    }
}