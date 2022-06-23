using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetAllUserTodoSubtasksHandler : BaseHandler<SubTask>, IRequestHandler<GetAllUserTodoSubtasksQuery, IEnumerable<SubTaskResponseDto>>
{
    private readonly IRepository<Todo> _todoRepo;
    private readonly HttpContext _context;
    private readonly IRepository<User> _userRepo;

    public GetAllUserTodoSubtasksHandler(IMapper mapper, IRepository<SubTask> repo, IRepository<Todo> todoRepo, IRepository<User> userRepo, IHttpContextAccessor httpContextAccessor)
            : base(mapper, repo)
    {
        _context = httpContextAccessor.HttpContext;
        _userRepo = userRepo;
        _todoRepo = todoRepo;
    }

    public async Task<IEnumerable<SubTaskResponseDto>> Handle(GetAllUserTodoSubtasksQuery request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = (await _userRepo.GetAll(u => u.Username.Equals(currentUserName))).Single();
        var result = await _repo.GetAll(u => u.Todo.User.Id == currentUser.Id);
        return _mapper.Map<IEnumerable<SubTaskResponseDto>>(result);
    }
}