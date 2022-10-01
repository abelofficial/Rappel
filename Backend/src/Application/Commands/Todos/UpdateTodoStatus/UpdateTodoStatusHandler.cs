using System.Net;
using API.Application.Queries;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateTodoStatusHandler : BaseHandler<Todo>, IRequestHandler<UpdateTodoStatusCommand, TodoResponseDto>
{
    private readonly IMediator _mediator;
    public UpdateTodoStatusHandler(IMapper mapper, IRepository<Todo> db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<TodoResponseDto> Handle(UpdateTodoStatusCommand request, CancellationToken cancellationToken)
    {

        var currentUser = await _mediator.Send(new CurrentUserQuery());

        try
        {
            var todoItem = await _db.GetOne(td => td.User.Id == currentUser.Id &&
                                            td.Id == request.Id &&
                                            td.Project.Id == request.ProjectId);
            todoItem.Status = request.Status;
            var result = _db.Update(todoItem);
            await _db.SaveChanges();
            return _mapper.Map<TodoResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A Todo item with the id {request.Id} in project {request.ProjectId} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}
