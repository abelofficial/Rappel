using API.Application.Commands;
using API.Application.Queries;
using API.Application.Results;
using API.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Produces("application/json")]
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

    /// <summary>
    /// Get a user by id.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseDto))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetUserById(int id)
    {
        return Ok(await _mediator.Send(new GetUserByIdQuery() { Id = id }));
    }

    /// <summary>
    /// Updated current user information.
    /// </summary>
    [HttpPut()]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserResponseDto>))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UpdateCurrentUserInfo(UpdateUserInfoCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

}
