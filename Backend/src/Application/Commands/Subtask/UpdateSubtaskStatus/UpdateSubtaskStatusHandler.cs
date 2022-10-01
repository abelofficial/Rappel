using System.Net;
using API.Application.Queries;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateSubtaskStatusHandler : BaseHandler<SubTask>, IRequestHandler<UpdateSubtaskStatusCommand, SubTaskResponseDto>
{
    private readonly IMediator _mediator;
    public UpdateSubtaskStatusHandler(IMapper mapper, IRepository<SubTask> db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<SubTaskResponseDto> Handle(UpdateSubtaskStatusCommand request, CancellationToken cancellationToken)
    {

        var currentUser = await _mediator.Send(new CurrentUserQuery());
        try
        {
            var todoItem = await _db.GetOne(st => st.Todo.User.Id == currentUser.Id &&
                                                   st.Todo.Id == request.TodoId &&
                                                    st.Id == request.SubTaskId);
            todoItem.Status = request.Status;
            var result = _db.Update(todoItem);
            await _db.SaveChanges();
            return _mapper.Map<SubTaskResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A Subtask item with the id {request.SubTaskId} for the todo item id {request.TodoId} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}
