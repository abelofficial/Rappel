using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;
namespace API.Application.Queries;

public class GetAllSubtasksHandler : BaseHandler<SubTask>, IRequestHandler<GetAllSubtasksQuery, IEnumerable<SubTaskResponseDto>>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Todo> _todoDb;

    public GetAllSubtasksHandler(IMapper mapper, IRepository<SubTask> db, IRepository<Todo> todoDb, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
        _todoDb = todoDb;
    }

    public async Task<IEnumerable<SubTaskResponseDto>> Handle(GetAllSubtasksQuery request, CancellationToken cancellationToken)
    {

        var currentUser = await _mediator.Send(new CurrentUserQuery());

        if (!await _todoDb.IsTrue(td => td.Id == request.Id))
            throw new HttpRequestException($"Todo item with id {request.Id} doesn't exist", null, HttpStatusCode.NotFound);

        var result = await _db.GetAll(st => st.Todo.Id == request.Id);
        return _mapper.Map<IEnumerable<SubTaskResponseDto>>(result);
    }
}