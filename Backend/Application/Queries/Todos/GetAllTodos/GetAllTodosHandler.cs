using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetAllTodosHandler : BaseHandler<Todo>, IRequestHandler<GetAllTodosQuery, IEnumerable<TodoResponseDto>>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Project> _projectDb;

    public GetAllTodosHandler(IMapper mapper, IRepository<Todo> db, IRepository<Project> projectDb, IMediator mediator) : base(mapper, db)
    {
        _mediator = mediator;
        _projectDb = projectDb;
    }

    public async Task<IEnumerable<TodoResponseDto>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());

        if (!await _projectDb.IsTrue(p => p.Id == request.ProjectId && p.Owner.Id == currentUser.Id))
            throw new HttpRequestException($"Project with id {request.ProjectId} doesn't exist", null, HttpStatusCode.NotFound);

        var result = await _db
        .GetAll(td => td.Project.Id == request.ProjectId && td.User.Id == currentUser.Id);


        return result.Select(u => _mapper.Map<TodoResponseDto>(u));
    }
}