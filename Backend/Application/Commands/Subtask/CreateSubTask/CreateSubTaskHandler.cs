using API.Application.Queries;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class CreateSubTaskHandler : BaseHandler<SubTask>, IRequestHandler<CreateSubTaskCommand, SubTaskResponseDto>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Todo> _todoDb;

    public CreateSubTaskHandler(IMapper mapper, IRepository<SubTask> db, IRepository<Todo> todoDb, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
        _todoDb = todoDb;
    }

    public async Task<SubTaskResponseDto> Handle(CreateSubTaskCommand request, CancellationToken cancellationToken)
    {
        var currentUSer = await _mediator.Send(new CurrentUserQuery());
        var parentTodoItem = await _mediator.Send(new GetTodoQuery()
        {
            Id = request.ParentId,
            ProjectId = request.ProjectId,
        });

        var newSubtaskItem = _mapper.Map<SubTask>(request);
        newSubtaskItem.Todo = await _todoDb
            .GetOne(td => td.Id == parentTodoItem.Id);

        var result = _db.Create(newSubtaskItem);
        await _db.SaveChanges();

        return _mapper.Map<SubTaskResponseDto>(result);
    }
}
