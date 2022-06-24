using System.Net;
using API.Application.Commands;
using API.Data;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Api.Tests.CommandsTest;

public class LoginUserTest : SqliteInMemory
{

    private readonly Mock<IConfiguration> _configMock;
    private readonly AppDbContext _context;

    public LoginUserTest()
    {
        _configMock = new Mock<IConfiguration>();
        _context = new AppDbContext(ContextOptions);
    }

    [Fact]
    public async void login_user_handler_should_throw_unauthorized_on_user_not_found()
    {
        // Arrange

        var handler = new LoginUserHandler(_mapper, _context, _configMock.Object);
        var command = new LoginUserCommand()
        { };

        // Act
        var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await handler.Handle(command, CancellationToken.None));

        // Assert
        ((HttpRequestException)exception).StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void login_user_handler_should_throw_unauthorized_on_wrong_password()
    {
        // Arrange

        var handler = new LoginUserHandler(_mapper, _context, _configMock.Object);
        var command = new LoginUserCommand()
        {
            Username = "TestUser",
            Password = "WrongPassword"
        };

        // Act
        var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await handler.Handle(command, CancellationToken.None));

        // Assert
        ((HttpRequestException)exception).StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}