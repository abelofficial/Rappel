using System.Net;
using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetUserTodoHandler : BaseHandler<Todo>, IRequestHandler<GetUserTodoQuery, TodoResponseDto>
{
    private readonly IMediator _mediator;

    public GetUserTodoHandler(IMapper mapper, IRepository<Todo> repo, IMediator mediator)
            : base(mapper, repo)
    {
        _mediator = mediator;
    }

    public async Task<TodoResponseDto> Handle(GetUserTodoQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());

        try
        {
            var result = (await _repo.GetOne(u => u.User.Id == currentUser.Id && u.Id == request.Id && u.SubTodoList.Count() == 0));

            return _mapper.Map<TodoResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A Todo item with the id {request.Id} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}