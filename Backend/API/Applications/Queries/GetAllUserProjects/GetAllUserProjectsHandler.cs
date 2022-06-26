using API.Application.Results;
using API.Data;
using API.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetAllUserProjectsHandler : BaseHandler<User>, IRequestHandler<GetAllUserProjectsQuery, IEnumerable<ProjectResponseDto>>
{
    private readonly IMediator _mediator;
    public GetAllUserProjectsHandler(IMapper mapper, AppDbContext db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<ProjectResponseDto>> Handle(GetAllUserProjectsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());
        var result = await _db.Projects.Where(p => p.Members.Any(m => m.Id == currentUser.Id)).Include(p => p.Items).Include(p => p.Owner).ToListAsync();

        return result.Select(u => _mapper.Map<ProjectResponseDto>(u));
    }
}