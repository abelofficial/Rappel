using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetProjectHandler : BaseHandler<Todo>, IRequestHandler<GetProjectQuery, ProjectResponseDto>
{

    private readonly IMediator _mediator;

    public GetProjectHandler(IMapper mapper, AppDbContext db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<ProjectResponseDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());

        try
        {
            var result = await _db.Projects
            .Include(p => p.Owner)
            .Include(p => p.Items)
            .Include(p => p.Members)
            .Where(p => p.Owner.Id == currentUser.Id || p.Members.Any(m => m.Id == currentUser.Id))
            .SingleAsync(p => p.Id == request.Id);

            return _mapper.Map<ProjectResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"Project with the id {request.Id} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }


}