using System.Net;
using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetUserTodoSubtaskHandler : BaseHandler<SubTask>, IRequestHandler<GetUserTodoSubtaskQuery, SubTaskResponseDto>
{
    private readonly IRepository<Todo> _todoRepo;
    private readonly HttpContext _context;
    private readonly IRepository<User> _userRepo;

    public GetUserTodoSubtaskHandler(IMapper mapper, IRepository<SubTask> repo, IRepository<Todo> todoRepo, IRepository<User> userRepo, IHttpContextAccessor httpContextAccessor)
            : base(mapper, repo)
    {
        _context = httpContextAccessor.HttpContext;
        _userRepo = userRepo;
        _todoRepo = todoRepo;
    }

    public async Task<SubTaskResponseDto> Handle(GetUserTodoSubtaskQuery request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = (await _userRepo.GetAll(u => u.Username.Equals(currentUserName))).Single();
        var parentTodoItem = await _todoRepo.GetOne(td => td.Id == request.TodoId);

        try
        {
            var result = await _repo.GetAllWith<Todo>(u => u.Todo.User.Id == currentUser.Id);
            return _mapper.Map<SubTaskResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A subtask with id {request.SubTaskId} for the Todo item id {request.TodoId} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}