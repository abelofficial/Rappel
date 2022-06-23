using System.Net;
using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetUserTodoSubtaskHandler : BaseHandler<Todo>, IRequestHandler<GetUserTodoSubtaskQuery, TodoResponseDto>
{
    private readonly IMediator _mediator;

    public GetUserTodoSubtaskHandler(IMapper mapper, IRepository<Todo> repo, IMediator mediator)
            : base(mapper, repo)
    {
        _mediator = mediator;
    }

    public async Task<TodoResponseDto> Handle(GetUserTodoSubtaskQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());

        try
        {
            var result = (await _repo.GetOne(u => u.User.Id == currentUser.Id && u.Id == request.Id && u.SubTodoList.Any(st => st.Id == request.SubTaskId)));

            return _mapper.Map<TodoResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A subtask with id {request.SubTaskId} for the Todo item id {request.Id} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}