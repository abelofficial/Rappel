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
public class ProjectsController : ControllerBase
{
    private readonly ILogger<ProjectsController> _logger;
    private readonly IMediator _mediator;

    public ProjectsController(ILogger<ProjectsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Create Project item.
    /// </summary>
    [HttpPost()]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionMessage))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> CreateProjectItem(CreateProjectCommand request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(GetUserProject), new { id = response.Id }, response);

    }

    /// <summary>
    // Get all user Projects
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectResponseDto>))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetAllUserProjects()
    {
        var response = await _mediator.Send(new GetAllUserProjectsQuery());
        return Ok(response);
    }


    /// <summary>
    /// Update Todo items progress status.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(typeof(ExceptionMessage), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetUserProject(int id)
    {
        var response = await _mediator.Send(new GetUserProjectQuery { Id = id });
        return Ok(response);
    }


}
