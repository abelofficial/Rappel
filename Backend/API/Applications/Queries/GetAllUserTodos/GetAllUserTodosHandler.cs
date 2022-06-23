using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetAllUserTodosHandler : BaseHandler<Todo>, IRequestHandler<GetAllUserTodosQuery, IEnumerable<TodoResponseDto>>
{
    private readonly IMediator _mediator;

    public GetAllUserTodosHandler(IMapper mapper, IRepository<Todo> repo, IMediator mediator)
            : base(mapper, repo)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<TodoResponseDto>> Handle(GetAllUserTodosQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());
        var result = await _repo.GetAll(u => u.User.Id == currentUser.Id);

        return result.Select(u => _mapper.Map<TodoResponseDto>(u));
    }
}