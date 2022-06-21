using API.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
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
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(RegisterUserCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    /// <summary>
    /// Authenticate user.
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(LoginUserCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
    /// <summary>
    /// Test auth endpoint. (ToDo: remove)
    /// </summary>

    [HttpPost("TestAuth")]
    [Authorize]
    public ActionResult TestAuth()
    {
        return Ok(new { Message = "You are now authenticated." });
    }
}
