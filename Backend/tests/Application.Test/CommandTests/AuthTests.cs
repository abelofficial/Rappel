using System.Linq.Expressions;
using System.Net;
using API.Application;
using API.Application.Commands;
using API.Domain.Entities;
using API.Domain.ValueObjects;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Application.Tests;

public class AuthTests : BaseHandlerTest
{
    private readonly Mock<IConfiguration> _configMock;
    private readonly Mock<IRepository<User>> _dbMock;
    private readonly Password _testPassword;
    public AuthTests() : base()
    {
        _testPassword = new Password("test");
        _configMock = new Mock<IConfiguration>();
        _dbMock = new Mock<IRepository<User>>();
        _configMock.SetupGet(x => x[It.Is<string>(s => s == "JWT:secret")])
            .Returns("test-jwt-token-3.k{efCnVT5)}mw");
    }

    private void setupHappyDbMock()
    {
        _dbMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new User()
            {
                Id = 1,
                Username = "test",
                PasswordHash = _testPassword.PasswordHash,
                PasswordSalt = _testPassword.PasswordSalt
            });
    }

    private void setupSadDbMock()
    {
        _dbMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(() => null);
    }

    private void setupHappyDbCheckMock()
    {
        _dbMock.Setup(x => x.IsTrue(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(false);
    }

    private void setupSadDbCheckMock()
    {
        _dbMock.Setup(x => x.IsTrue(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(true);
    }

    [Fact]
    public async void
    Happy_login_user_handler_should_return_token_on_successful_login()
    {
        // Arrange
        setupHappyDbMock();
        var command = new LoginUserCommand() { Username = "test", Password = "test" };

        var handler =
            new LoginUserHandler(_mapper, _dbMock.Object, _configMock.Object);

        // Act
        var resp = await handler.Handle(command, CancellationToken.None);

        // Assert
        resp.Token.Should().NotBeNull();
    }

    [Fact]
    public async void
    Happy_register_user_handler_should_return_user_on_successful_login()
    {
        // Arrange
        setupHappyDbCheckMock();
        var command = new RegisterUserCommand()
        {
            Username = "test",
            Password = "test",
            Email = "test@test.com",
            FirstName = "test",
            LastName = "test"
        };

        var handler =
            new RegisterUserHandler(_mapper, _dbMock.Object);

        // Act
        var resp = await handler.Handle(command, CancellationToken.None);

        // Assert
        resp.Username.Should().Be("test");
        resp.Email.Should().Be("test@test.com");
        resp.FirstName.Should().Be("test");
        resp.LastName.Should().Be("test");
        resp.FullName.Should().Be("test test");
    }

    [Fact]
    public async void
    Sad_register_user_handler_should_throw_badrequest_on_user_conflict()
    {
        // Arrange
        setupSadDbCheckMock();
        var command = new RegisterUserCommand();

        var handler =
            new RegisterUserHandler(_mapper, _dbMock.Object);

        // Act
        var exception = await Assert.ThrowsAsync<HttpRequestException>(
            async () => await handler.Handle(command, CancellationToken.None));

        // Assert
        ((HttpRequestException)exception)
            .StatusCode.Should()
            .Be(HttpStatusCode.BadRequest);
        ((HttpRequestException)exception)
            .Message.Should()
            .Be("Username is already taken.\n" +
                "Email address is already taken.");
    }

    [Fact]
    public async void
    Sad_login_user_handler_should_throw_unauthorized_on_incorrect_password()
    {
        // Arrange
        setupHappyDbMock();
        var command =
            new LoginUserCommand() { Username = "test", Password = "UNKNOWN" };

        var handler =
            new LoginUserHandler(_mapper, _dbMock.Object, _configMock.Object);

        // Act
        var exception = await Assert.ThrowsAsync<HttpRequestException>(
            async () => await handler.Handle(command, CancellationToken.None));

        // Assert
        ((HttpRequestException)exception)
            .StatusCode.Should()
            .Be(HttpStatusCode.Unauthorized);
        ((HttpRequestException)exception)
            .Message.Should().Be("Wrong credential");
    }

    [Fact]
    public async void
    Sad_login_user_handler_should_throw_unauthorized_on_user_not_found()
    {
        // Arrange
        setupSadDbMock();
        var command = new LoginUserCommand() { Username = "test", Password = "test" };

        var handler =
            new LoginUserHandler(_mapper, _dbMock.Object, _configMock.Object);

        // Act
        var exception = await Assert.ThrowsAsync<HttpRequestException>(
            async () => await handler.Handle(command, CancellationToken.None));

        // Assert
        ((HttpRequestException)exception)
            .StatusCode.Should()
            .Be(HttpStatusCode.Unauthorized);
        ((HttpRequestException)exception)
            .Message.Should().Be("Wrong credential");
    }
}
