using API.Application.Commands;
using API.Application.Dtos.CommandsDtos;
using API.Application.Queries;
using API.Application.Results;
using API.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Produces("application/json")]
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
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubTaskResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionMessage))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> CreateSubTaskItem(int id, CreateSubtaskRequestDto request)
    {
        var response = await _mediator.Send(new CreateSubTaskCommand() { ParentId = id, Title = request.Title, Description = request.Description });
        return CreatedAtAction(nameof(GetUserTodoSubTaskItem), new { id = response.TodoId, subtaskId = response.Id }, response);
    }

    /// <summary>
    /// Update subtask item
    /// </summary>
    [HttpPut("{id}/[controller]/{subtaskId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubTaskResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UpdateSubtaskItemItem(int id, int subtaskId, UpdateSubtaskRequestDto request)
    {
        var response = await _mediator.Send(new UpdateSubtaskCommand()
        {
            TodoId = id,
            SubTaskId = subtaskId,
            Title = request.Title,
            Description = request.Description
        });
        return Ok(response);
    }

    /// <summary>
    /// Update SUbtask items progress status
    /// </summary>
    [HttpPatch("{id}/[controller]/{subtaskId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubTaskResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> PatchSubtaskStatusItem(int id, int subtaskId, UpdateTodoStatusRequestDto request)
    {
        var response = await _mediator.Send(new UpdateSubtaskStatusCommand() { TodoId = id, SubTaskId = subtaskId, Status = request.Status });
        return Ok(response);
    }

    /// <summary>
    // Get a subtask item user todo item
    /// </summary>
    [HttpGet("{id}/[controller]/{subtaskId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubTaskResponseDto))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SubTaskResponseDto>))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetAllUserTodoSubtasks(int id)
    {
        var response = await _mediator.Send(new GetAllUserTodoSubtasksQuery() { Id = id });
        return Ok(response);
    }
}
