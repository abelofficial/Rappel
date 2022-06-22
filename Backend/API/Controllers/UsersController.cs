using API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IMediator _mediator;

    public UsersController(ILogger<UsersController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Query all users.
    /// </summary>

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetAllUsers(string? filter)
    {
        return Ok(await _mediator.Send(new GetAllUsersQuery() { Filter = filter }));
    }

}
