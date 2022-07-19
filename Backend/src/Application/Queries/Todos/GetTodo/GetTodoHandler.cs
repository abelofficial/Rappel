using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetTodoHandler : BaseHandler<Todo>, IRequestHandler<GetTodoQuery, TodoResponseDto>
{
    private readonly IMediator _mediator;

    public GetTodoHandler(IMapper mapper, IRepository<Todo> db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<TodoResponseDto> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {

        var currentUser = await _mediator.Send(new CurrentUserQuery());
        try
        {
            var result = await _db
            .GetAll(td => td.Project.Owner.Id == currentUser.Id ||
                        td.Project.Members.Any(m => m.Id == currentUser.Id) &&
                        td.Id == request.Id && td.Project.Id == request.ProjectId);

            return _mapper.Map<TodoResponseDto>(result.Single());
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A Todo item with the id {request.Id} in project ${request.ProjectId} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}