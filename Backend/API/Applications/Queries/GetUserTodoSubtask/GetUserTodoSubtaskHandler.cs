using System.Net;
using API.Application.Results;
using API.Data;
using API.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetUserTodoSubtaskHandler : BaseHandler<SubTask>, IRequestHandler<GetUserTodoSubtaskQuery, SubTaskResponseDto>
{
    private readonly HttpContext _context;

    public GetUserTodoSubtaskHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<SubTaskResponseDto> Handle(GetUserTodoSubtaskQuery request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));
        var parentTodoItem = await _db.Todos.SingleAsync(td => td.Id == request.TodoId);

        try
        {
            var result = await _db.SubTasks.Where(st => st.Id == request.SubTaskId).Where(u => u.Todo.User.Id == currentUser.Id).Where(u => u.Todo.Id == request.TodoId).SingleAsync();
            return _mapper.Map<SubTaskResponseDto>(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new HttpRequestException($"A subtask with id {request.SubTaskId} for the Todo item id {request.TodoId} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}