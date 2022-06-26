using API.Application.Commands;
using API.Application.Dtos.CommandsDtos;
using API.Application.Queries;
using API.Application.Results;
using API.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("project/")]
public class TodosController : ControllerBase
{
    private readonly ILogger<TodosController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TodosController(ILogger<TodosController> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Create Todo item.
    /// </summary>
    [HttpPost("{id}/[controller]")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionMessage))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> CreateTodoItem(int id, CreateTodoRequestDto request)
    {
        var command = _mapper.Map<CreateTodoCommand>(request);
        command.ProjectId = id;
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUserTodo), new { id = response.Id, todoId = id, }, response);
    }

    /// <summary>
    /// Update Todo items progress status.
    /// </summary>
    [HttpPatch("{id}/[controller]/{todoId}/status")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> CreateTodoItem(int id, int todoId, UpdateTodoStatusRequestDto request)
    {
        var response = await _mediator.Send(new UpdateTodoStatusCommand()
        {
            Id = todoId,
            Status = request.Status,
            ProjectId = id,
        });
        return Ok(response);
    }

    /// <summary>
    // Get all user todos
    /// </summary>
    [HttpGet("{id}/[controller]")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoResponseDto>))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetAllUserTodos(int id)
    {
        var response = await _mediator.Send(new GetAllUserTodosQuery() { ProjectId = id });
        return Ok(response);
    }

    /// <summary>
    // Get user Todo item.
    /// </summary>
    [HttpGet("{id}/[controller]/{todoId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoResponseDto))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetUserTodo(int id, int todoId)
    {
        var response = await _mediator.Send(new GetUserTodoQuery() { Id = todoId, ProjectId = id });
        return Ok(response);
    }
}
