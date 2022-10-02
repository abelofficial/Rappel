using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.RestApi.Controllers;

[Route("[controller]")]
[ApiController]
[Produces("application/json")]

public abstract class BaseController : ControllerBase
{

    protected readonly IMediator _mediator;

    protected readonly IMapper? _mapper;

    public BaseController(IMediator mediator, IMapper mapper)
    {

        _mediator = mediator;
        _mapper = mapper;
    }

    public BaseController(IMediator mediator)
    {

        _mediator = mediator;
    }


}
