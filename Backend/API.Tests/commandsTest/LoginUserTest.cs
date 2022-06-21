using System.Linq.Expressions;
using System.Net;
using API.Application.Commands;
using API.Application.Profiles;
using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using API.Data.ValueObjects;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Api.Tests.CommandsTest;

public class LoginUserTest
{
    private readonly Mock<IRepository<User>> _repoMock;
    private readonly Mock<IConfiguration> _configMock;
    private readonly IMapper _mapper;

    public LoginUserTest()
    {

        _repoMock = new Mock<IRepository<User>>();
        _configMock = new Mock<IConfiguration>();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<CommandToEntity>();
            c.AddProfile<EntityToResult>();
        });
        _mapper = mapperConfig.CreateMapper();

        _configMock.SetupGet(x => x[It.Is<string>(s => s == "JWT:secret")]).Returns("test-jwt-token-3.k{efCnVT5)}mw");
    }

    private void setupMockHappy()
    {
        var password = new Password("TestPassword");

        _repoMock.Setup(c => c.GetAll(It.Is<Expression<Func<User, bool>>>(u => true)))
        .ReturnsAsync(() => new List<User> { new User() { Id = 0, Username = "test-user", PasswordHash = password.PasswordHash, PasswordSalt = password.PasswordSalt } });
    }

    private void setupMockSad()
    {
        _repoMock.Setup(c => c.GetAll(It.Is<Expression<Func<User, bool>>>(u => true)))
        .ReturnsAsync(() => new List<User> { });
    }

    [Fact]
    public async void login_user_handler_should_return_resultDto_on_success()
    {
        // Arrange
        setupMockHappy();
        var handler = new LoginUserHandler(_mapper, _repoMock.Object, _configMock.Object);
        var command = new LoginUserCommand()
        {
            Password = "TestPassword"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeOfType<LoginResponseDto>();
    }

    [Fact]
    public async void login_user_handler_should_throw_unauthorized_on_user_not_found()
    {
        // Arrange
        setupMockSad();
        var handler = new LoginUserHandler(_mapper, _repoMock.Object, _configMock.Object);
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
        setupMockHappy();
        var handler = new LoginUserHandler(_mapper, _repoMock.Object, _configMock.Object);
        var command = new LoginUserCommand()
        {
            Password = "WrongPassword"
        };

        // Act
        var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await handler.Handle(command, CancellationToken.None));

        // Assert
        ((HttpRequestException)exception).StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}