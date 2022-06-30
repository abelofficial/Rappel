using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetTodoHandler : BaseHandler<Todo>, IRequestHandler<GetTodoQuery, TodoResponseDto>
{
    private readonly IMediator _mediator;

    public GetTodoHandler(IMapper mapper, AppDbContext db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<TodoResponseDto> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {

        var currentUser = await _mediator.Send(new CurrentUserQuery());
        try
        {
            var result = await _db.Todos
            .Include(td => td.SubTask)
            .Include(td => td.User)
            .Where(td => td.Project.Owner.Id == currentUser.Id ||
                        td.Project.Members.Any(m => m.Id == currentUser.Id))
            .SingleAsync(td => td.Id == request.Id && td.Project.Id == request.ProjectId);

            return _mapper.Map<TodoResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A Todo item with the id {request.Id} in project ${request.ProjectId} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}