namespace Main.User;

/// <summary>
/// A user
/// </summary>
/// <param name="Id">The user's random GUID</param>
/// <param name="Name">The user's name</param>
/// <param name="Age">The user's age</param>
public record User(Guid Id, string Name, int Age)
{
    // Constructor for SQLite
    User(string Id, string Name, Int64 Age) : this(Guid.Parse(Id), Name, (int) Age) {}
}