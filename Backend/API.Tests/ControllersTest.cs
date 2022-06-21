using API.Application.Commands;
using API.Application.Results;
using API.Controllers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace Api.Tests;

public class ControllersTest
{
    private readonly Mock<IMediator> _mock;
    public ControllersTest()
    {
        _mock = new Mock<IMediator>();
        setupMock();
    }

    private void setupMock()
    {
        _mock.Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), default(CancellationToken))).ReturnsAsync(new UserResponseDto());
        _mock.Setup(m => m.Send(It.IsAny<LoginUserCommand>(), default(CancellationToken))).ReturnsAsync(new LoginResponseDto());
    }

    [Fact]
    public async void register_user_should_call_mediator_handler()
    {
        // Arrange
        var controller = new AuthController(It.IsAny<ILogger<AuthController>>(), _mock.Object);

        // Act
        await controller.RegisterUser(It.IsAny<RegisterUserCommand>());

        // Assert
        _mock.Verify(x => x.Send(It.IsAny<RegisterUserCommand>(), default(CancellationToken)), Times.Once());
    }

    [Fact]
    public async void login_user_should_call_mediator_handler()
    {
        // Arrange
        var controller = new AuthController(It.IsAny<ILogger<AuthController>>(), _mock.Object);

        // Act
        await controller.LoginUser(It.IsAny<LoginUserCommand>());

        // Assert
        _mock.Verify(x => x.Send(It.IsAny<LoginUserCommand>(), default(CancellationToken)), Times.Once());
    }
}