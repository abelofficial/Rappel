using API.Application.Commands;
using API.Application.Dtos;
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
public class TodosController : ControllerBase
{
    private readonly ILogger<TodosController> _logger;
    private readonly IMediator _mediator;

    public TodosController(ILogger<TodosController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Create Todo item.
    /// </summary>
    [HttpPost()]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionMessage))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> CreateTodoItem(CreateTodoCommand request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetUserTodo), new { id = response.Id }, response);
    }

    /// <summary>
    /// Update Todo items progress status.
    /// </summary>
    [HttpPatch("{id}/status")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> CreateTodoItem(int id, UpdateTodoStatusRequestDto request)
    {
        var response = await _mediator.Send(new UpdateTodoStatusCommand() { Id = id, Status = request.Status });
        return Ok(response);
    }

    /// <summary>
    // Get all user todos
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoResponseDto>))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetAllUserTodos()
    {
        var response = await _mediator.Send(new GetAllUserTodosQuery());
        return Ok(response);
    }

    /// <summary>
    // Get user Todo item.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoResponseDto))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetUserTodo(int id)
    {
        var response = await _mediator.Send(new GetUserTodoQuery() { Id = id });
        return Ok(response);
    }
}
