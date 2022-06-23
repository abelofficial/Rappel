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
}
