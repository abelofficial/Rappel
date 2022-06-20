using API.Application.Commands;
using MediatR;
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
    [HttpPost]
    public async Task<ActionResult> RegisterUser(RegisterUserCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
}
