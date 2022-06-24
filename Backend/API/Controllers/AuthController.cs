using API.Application.Commands;
using API.Application.Queries;
using API.Application.Results;
using API.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[Route("[controller]"), Authorize]
[ApiController]
[Produces("application/json")]

public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMediator _mediator;

    public AuthController(ILogger<AuthController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Register new user.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponseDto))]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(RegisterUserCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    /// <summary>
    /// Authenticate user.
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> LoginUser(LoginUserCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
    /// <summary>
    /// Get the authenticated user
    /// </summary>

    [Authorize]
    [HttpGet("user")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseDto))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> CurrentUser()
    {
        return Ok(await _mediator.Send(new CurrentUserQuery()));
    }
}
