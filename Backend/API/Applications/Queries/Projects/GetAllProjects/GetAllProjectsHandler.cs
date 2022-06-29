using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetAllProjectsHandler : BaseHandler<User>, IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectResponseDto>>
{
    private readonly IMediator _mediator;
    public GetAllProjectsHandler(IMapper mapper, AppDbContext db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<ProjectResponseDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());
        var result = await _db.Projects
        .Include(p => p.Items)
        .Include(p => p.Members)
        .Where(p => p.Owner.Id == currentUser.Id)
        .ToListAsync();

        return result.Select(u => _mapper.Map<ProjectResponseDto>(u));
    }
}