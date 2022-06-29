using API.Application.Queries;
using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Commands;

public class CreateSubTaskHandler : BaseHandler<SubTask>, IRequestHandler<CreateSubTaskCommand, SubTaskResponseDto>
{
    private readonly IMediator _mediator;
    public CreateSubTaskHandler(IMapper mapper, AppDbContext db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator; ;
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
        newSubtaskItem.Todo = await _db.Todos
            .SingleAsync(td => td.Id == parentTodoItem.Id);

        var result = _db.SubTasks.Add(newSubtaskItem).Entity;
        await _db.SaveChangesAsync();

        return _mapper.Map<SubTaskResponseDto>(result);
    }
}
