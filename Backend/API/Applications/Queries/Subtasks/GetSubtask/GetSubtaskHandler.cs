using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetSubtaskHandler : BaseHandler<SubTask>, IRequestHandler<GetSubtaskQuery, SubTaskResponseDto>
{
    private readonly IMediator _mediator;

    public GetSubtaskHandler(IMapper mapper, AppDbContext db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<SubTaskResponseDto> Handle(GetSubtaskQuery request, CancellationToken cancellationToken)
    {

        var currentUser = await _mediator.Send(new CurrentUserQuery());
        var parentTodoItem = await _mediator.Send(new GetTodoQuery()
        {
            Id = request.TodoId,
            ProjectId = request.ProjectId
        });

        try
        {
            var result = await _db.SubTasks
             .Include(st => st.Todo.Project)
             .Where(st => st.Todo.Id == request.TodoId)
             .SingleAsync(st => st.Id == request.SubTaskId);
            return _mapper.Map<SubTaskResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A subtask item with id {request.SubTaskId} doesn't exist", null, HttpStatusCode.NotFound);
        }

    }
}