using API.Application.Queries;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateTodoHandler : BaseHandler<Todo>, IRequestHandler<UpdateTodoCommand, TodoResponseDto>
{

    private readonly IMediator _mediator;

    public UpdateTodoHandler(IMapper mapper, IRepository<Todo> db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<TodoResponseDto> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {

        var todo = await _mediator.Send(new GetTodoQuery()
        {
            Id = request.Id,
            ProjectId = request.ProjectId
        });

        var todoItem = await _db.GetOne(todo.Id);
        _mapper.Map(request, todoItem);
        var updatedItem = _db.Update(todoItem);
        await _db.SaveChanges();

        return _mapper.Map<TodoResponseDto>(updatedItem);
    }
}
