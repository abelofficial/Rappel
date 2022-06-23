using API.Application.Commands;
using API.Application.Dtos;
using API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("todo/")]
public class TodosSubtasksController : ControllerBase
{
    private readonly ILogger<TodosSubtasksController> _logger;
    private readonly IMediator _mediator;

    public TodosSubtasksController(ILogger<TodosSubtasksController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    // Create a subtask for a todo item.
    /// </summary>
    [HttpPost("{id}/[controller]")]
    [Authorize]
    public async Task<ActionResult> CreateSubTaskItem(int id, CreateTodoCommand request)
    {
        var response = await _mediator.Send(new CreateSubTaskCommand() { ParentId = id, Title = request.Title, Description = request.Description });
        return CreatedAtAction(nameof(GetUserTodoSubTaskItem), new { id = response.Todo.Id, subtaskId = response.Id }, response);
    }

    /// <summary>
    /// Update SUbtask items progress status
    /// </summary>
    [HttpPatch("{id}/[controller]/{subtaskId}")]
    [Authorize]
    public async Task<ActionResult> CreateTodoItem(int id, int subtaskId, UpdateTodoStatusRequestDto request)
    {
        var response = await _mediator.Send(new UpdateSubtaskStatusCommand() { TodoId = id, SubTaskId = subtaskId, Status = request.Status });
        return Ok(response);
    }

    /// <summary>
    // Get a subtask item user todo item
    /// </summary>
    [HttpGet("{id}/[controller]/{subtaskId}")]
    [Authorize]
    public async Task<ActionResult> GetUserTodoSubTaskItem(int id, int subtaskId)
    {
        var response = await _mediator.Send(new GetUserTodoSubtaskQuery() { TodoId = id, SubTaskId = subtaskId });
        return Ok(response);
    }

    /// <summary>
    // Get all subtask item for user todo item
    /// </summary>
    [HttpGet("{id}/[controller]")]
    [Authorize]
    public async Task<ActionResult> GetAllUserTodoSubtasks(int id)
    {
        var response = await _mediator.Send(new GetAllUserTodoSubtasksQuery() { Id = id });
        return Ok(response);
    }
}
