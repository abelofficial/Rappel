using API.Application.Commands;
using API.Application.Dtos;
using API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
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
    public async Task<ActionResult> CreateTodoItem(int id, UpdateTodoStatusRequestDto request)
    {
        var response = await _mediator.Send(new UpdateTodoStatusCommand() { Id = id, Status = request.Status });
        return Ok(response);
    }

    /// <summary>
    // Get all user todos
    /// </summary>
    [HttpGet()]
    [Authorize]
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
    public async Task<ActionResult> GetUserTodo(int id)
    {
        var response = await _mediator.Send(new GetUserTodoQuery() { Id = id });
        return Ok(response);
    }
}
