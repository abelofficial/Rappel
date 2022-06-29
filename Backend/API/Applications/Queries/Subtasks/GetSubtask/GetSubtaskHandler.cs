using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetSubtaskHandler : BaseHandler<SubTask>, IRequestHandler<GetSubtaskQuery, SubTaskResponseDto>
{
    private readonly HttpContext _context;

    public GetSubtaskHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<SubTaskResponseDto> Handle(GetSubtaskQuery request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));

        try
        {
            var parentTodoItem = await _db.Todos.SingleAsync(td => td.Id == request.TodoId);
            var result = await _db.SubTasks.Where(st => st.Id == request.SubTaskId).Where(u => u.Todo.User.Id == currentUser.Id).SingleAsync(u => u.Todo.Id == request.TodoId);
            return _mapper.Map<SubTaskResponseDto>(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new HttpRequestException($"A subtask with id {request.SubTaskId} for the Todo item id {request.TodoId} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}