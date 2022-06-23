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
    private readonly HttpContext _context;
    private readonly IRepository<User> _userRepo;

    public GetUserTodoHandler(IMapper mapper, IRepository<Todo> repo, IRepository<User> userRepo, IHttpContextAccessor httpContextAccessor)
            : base(mapper, repo)
    {
        _context = httpContextAccessor.HttpContext;
        _userRepo = userRepo;
    }

    public async Task<TodoResponseDto> Handle(GetUserTodoQuery request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = (await _userRepo.GetAll(u => u.Username.Equals(currentUserName))).Single();

        try
        {
            var result = (await _repo.GetOne(td => td.User.Id == currentUser.Id && td.Id == request.Id));

            return _mapper.Map<TodoResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A Todo item with the id {request.Id} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}