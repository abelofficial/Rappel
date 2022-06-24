using System.Net;
using API.Application.Commands;
using API.Data;
using FluentAssertions;

namespace Api.Tests.CommandsTest;

public class RegisterUserTest : SqliteInMemory
{
    private readonly AppDbContext _context;

    public RegisterUserTest()
    {
        _context = new AppDbContext(ContextOptions);

    }

    [Fact]
    public async void register_user_handler_should_throw_bad_request_on_conflicting_username_or_email()
    {
        // Arrange

        var handler = new RegisterUserHandler(_mapper, _context);
        var adminUser = MockUsers.GetAdminUser();

        var command = new RegisterUserCommand()
        {
            Password = "Pass1234",
            Username = adminUser.Username,
            Email = adminUser.Email,
            FirstName = adminUser.FirstName,
            LastName = adminUser.LastName,
        };

        // Act
        var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await handler.Handle(command, CancellationToken.None));

        // Assert
        ((HttpRequestException)exception).StatusCode.Should().Be(HttpStatusCode.BadRequest);
        ((HttpRequestException)exception).Message.Should().Be("Username is already taken.\nEmail address is already taken.");
    }
}