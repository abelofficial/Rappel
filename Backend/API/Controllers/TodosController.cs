using API.Application.Commands;
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
        return Ok(response);
    }

    /// <summary>
    // Create Todo item.
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

    /// <summary>
    // Create a subtask for a todo item.
    /// </summary>
    [HttpPost("{id}/subtask")]
    [Authorize]
    public async Task<ActionResult> CreateSubTaskItem(int id, CreateTodoCommand request)
    {
        var response = await _mediator.Send(new CreateSubTaskCommand() { ParentId = id, Title = request.Title, Description = request.Description });
        return Ok(response);
    }

    /// <summary>
    // Create a subtask for a todo item.
    /// </summary>
    [HttpGet("{id}/subtask/{subtaskId}")]
    [Authorize]
    public async Task<ActionResult> GetUserTodoSubTaskItem(int id, int subtaskId)
    {
        var response = await _mediator.Send(new GetUserTodoSubtaskQuery() { TodoId = id, SubTaskId = subtaskId });
        return Ok(response);
    }

    /// <summary>
    // Create a subtask for a todo item.
    /// </summary>
    [HttpGet("{id}/subtask")]
    [Authorize]
    public async Task<ActionResult> GetAllUserTodoSubtasks(int id)
    {
        var response = await _mediator.Send(new GetAllUserTodoSubtasksQuery() { Id = id });
        return Ok(response);
    }
}
