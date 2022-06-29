using API.Application.Queries;
using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateSubtaskHandler : BaseHandler<SubTask>, IRequestHandler<UpdateSubtaskCommand, SubTaskResponseDto>
{
    private readonly IMediator _mediator;

    public UpdateSubtaskHandler(IMapper mapper, AppDbContext db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<SubTaskResponseDto> Handle(UpdateSubtaskCommand request, CancellationToken cancellationToken)
    {
        var targetSubtask = await _mediator.Send(new GetSubtaskQuery()
        {
            TodoId = request.TodoId,
            SubTaskId = request.SubTaskId
        });
        var subTaskItem = await _db.SubTasks.FindAsync(targetSubtask.Id);
        _mapper.Map(request, subTaskItem);
        var result = _db.SubTasks.Update(subTaskItem).Entity;
        await _db.SaveChangesAsync();

        return _mapper.Map<SubTaskResponseDto>(result);
    }
}
