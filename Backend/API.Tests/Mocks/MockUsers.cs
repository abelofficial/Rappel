using API.Data.Entities;

public static class MockUsers
{

    public static User GetAdminUser() => new User()
    {
        Username = "Admin",
        FirstName = "Admin",
        LastName = "Admin",
        Email = "Admin",
        PasswordHash = new byte[0],
        PasswordSalt = new byte[0],
    };

    public static User GetUniqueUser(int id) => new User()
    {
        Username = $"user-{id}",
        FirstName = $"user-{id}",
        LastName = $"user-{id}",
        Email = $"user-{id}",
        PasswordHash = new byte[0],
        PasswordSalt = new byte[0],
    };

}
