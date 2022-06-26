using System.Net;
using API.Application.Results;
using API.Data;
using API.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetAllUserTodosHandler : BaseHandler<Todo>, IRequestHandler<GetAllUserTodosQuery, IEnumerable<TodoResponseDto>>
{
    private readonly IMediator _mediator;

    public GetAllUserTodosHandler(IMapper mapper, AppDbContext db, IMediator mediator) : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<TodoResponseDto>> Handle(GetAllUserTodosQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());

        if (!await _db.Projects.AnyAsync(p => p.Id == request.ProjectId &&
            p.Members.Any(m => m.Id == currentUser.Id)
          ))
            throw new HttpRequestException($"Project with id {request.ProjectId} doesn't exist", null, HttpStatusCode.NotFound);

        var result = await _db.Todos
        .Include(td => td.SubTask)
          .Where(td => td.Project.Id == request.ProjectId)
          .Where(u => u.User.Id == currentUser.Id).ToListAsync();

        return result.Select(u => _mapper.Map<TodoResponseDto>(u));
    }
}