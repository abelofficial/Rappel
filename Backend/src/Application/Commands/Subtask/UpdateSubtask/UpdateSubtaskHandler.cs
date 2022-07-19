using API.Application.Queries;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateSubtaskHandler : BaseHandler<SubTask>, IRequestHandler<UpdateSubtaskCommand, SubTaskResponseDto>
{
    private readonly IMediator _mediator;

    public UpdateSubtaskHandler(IMapper mapper, IRepository<SubTask> db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<SubTaskResponseDto> Handle(UpdateSubtaskCommand request, CancellationToken cancellationToken)
    {
        var targetSubtask = await _mediator.Send(new GetSubtaskQuery()
        {
            TodoId = request.TodoId,
            SubTaskId = request.SubTaskId,
            ProjectId = request.ProjectId
        });
        var subTaskItem = await _db.GetOne(targetSubtask.Id);
        _mapper.Map(request, subTaskItem);
        var result = _db.Update(subTaskItem);
        await _db.SaveChanges();

        return _mapper.Map<SubTaskResponseDto>(result);
    }
}
