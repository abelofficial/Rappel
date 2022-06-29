using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetAllTodosHandler : BaseHandler<Todo>, IRequestHandler<GetAllTodosQuery, IEnumerable<TodoResponseDto>>
{
    private readonly IMediator _mediator;

    public GetAllTodosHandler(IMapper mapper, AppDbContext db, IMediator mediator) : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<TodoResponseDto>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());

        if (!await _db.Projects.Include(p => p.Owner).Where(p => p.Owner.Id == currentUser.Id).AnyAsync(p => p.Id == request.ProjectId))
            throw new HttpRequestException($"Project with id {request.ProjectId} doesn't exist", null, HttpStatusCode.NotFound);

        var result = await _db.Todos
          .Include(td => td.SubTask)
          .Where(td => td.Project.Id == request.ProjectId)
          .Where(u => u.User.Id == currentUser.Id).ToListAsync();

        return result.Select(u => _mapper.Map<TodoResponseDto>(u));
    }
}