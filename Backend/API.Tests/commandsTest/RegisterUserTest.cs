using System.Linq.Expressions;
using System.Net;
using API.Application.Commands;
using API.Application.Profiles;
using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace Api.Tests.CommandsTest;

public class RegisterUserTest
{
    private readonly Mock<IRepository<User>> _mock;
    private readonly IMapper _mapper;

    public RegisterUserTest()
    {

        _mock = new Mock<IRepository<User>>();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<CommandToEntity>();
            c.AddProfile<EntityToResult>();
        });
        _mapper = mapperConfig.CreateMapper();

    }

    private void setupMockHappy()
    {

        _mock.Setup(c => c.GetAll(It.Is<Expression<Func<User, bool>>>(u => false)))
        .ReturnsAsync(() => new List<User> { });
    }

    private void setupMockSad()
    {

        _mock.Setup(c => c.GetAll(It.Is<Expression<Func<User, bool>>>(u => true)))
        .ReturnsAsync(() => new List<User> { new User() { } });
    }


    [Fact]
    public async void register_user_handler_should_return_resultDto_on_success()
    {
        // Arrange
        setupMockHappy();
        var handler = new RegisterUserHandler(_mapper, _mock.Object);
        var command = new RegisterUserCommand()
        {
            Password = "TestPassword"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeOfType<UserResponseDto>();
    }

    [Fact]
    public async void register_user_handler_should_throw_bad_request_on_conflicting_username_or_email()
    {
        // Arrange
        setupMockSad();
        var handler = new RegisterUserHandler(_mapper, _mock.Object);
        var command = new RegisterUserCommand()
        {
            Password = "TestPassword",
            Username = "fail"
        };

        // Act
        var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await handler.Handle(command, CancellationToken.None));

        // Assert
        ((HttpRequestException)exception).StatusCode.Should().Be(HttpStatusCode.BadRequest);
        ((HttpRequestException)exception).Message.Should().Be("Username is already taken.");
    }
}