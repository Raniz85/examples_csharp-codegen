using Main.User;

namespace Main.Tests;

public interface IDbEngine: IConnectionFactory, IAsyncLifetime
{
}