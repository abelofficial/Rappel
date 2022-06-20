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
    }

    private void setupMock()
    {
        _mock.Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), default(CancellationToken))).ReturnsAsync(new UserResponseDto());
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
}