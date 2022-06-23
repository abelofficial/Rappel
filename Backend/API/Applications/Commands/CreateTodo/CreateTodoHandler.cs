using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class CreateTodoHandler : BaseHandler<Todo>, IRequestHandler<CreateTodoCommand, TodoResponseDto>
{
    private readonly HttpContext _context;
    private readonly IRepository<User> _userRepo;
    public CreateTodoHandler(IMapper mapper, IRepository<Todo> repo, IRepository<User> userRepo, IHttpContextAccessor httpContextAccessor)
            : base(mapper, repo)
    {
        _context = httpContextAccessor.HttpContext;
        _userRepo = userRepo;
    }

    public async Task<TodoResponseDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var targetUser = await _userRepo.GetOne(u => u.Username.Equals(currentUserName));

        var newTodoItem = _mapper.Map<Todo>(request);
        newTodoItem.AddOwner(targetUser);
        var result = _repo.Create(newTodoItem);
        await _repo.SaveChanges();

        return _mapper.Map<TodoResponseDto>(result);
    }
}
