using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetAllProjectsHandler : BaseHandler<Project>, IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectResponseDto>>
{
    private readonly IMediator _mediator;
    public GetAllProjectsHandler(IMapper mapper, IRepository<Project> db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<ProjectResponseDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());
        var result = await _db.GetAll(p => p.Owner.Id == currentUser.Id);

        return result.Select(u => _mapper.Map<ProjectResponseDto>(u));
    }
}