using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetUserProjectHandler : BaseHandler<Todo>, IRequestHandler<GetUserProjectQuery, ProjectResponseDto>
{

    private readonly IMediator _mediator;

    public GetUserProjectHandler(IMapper mapper, AppDbContext db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<ProjectResponseDto> Handle(GetUserProjectQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());

        try
        {
            var result = await _db.Projects.Where(p => p.Members.Any(m => m.Id == currentUser.Id)).SingleAsync(p => p.Id == request.Id);

            return _mapper.Map<ProjectResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"Project with the id {request.Id} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }


}