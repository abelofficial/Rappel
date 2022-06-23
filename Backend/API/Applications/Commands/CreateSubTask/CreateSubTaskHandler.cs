using System.Net;
using API.Application.Results;
using API.Data;
using API.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Commands;

public class CreateSubTaskHandler : BaseHandler<SubTask>, IRequestHandler<CreateSubTaskCommand, SubTaskResponseDto>
{
    private readonly HttpContext _context;
    public CreateSubTaskHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<SubTaskResponseDto> Handle(CreateSubTaskCommand request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var targetUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));

        var newTodoItem = _mapper.Map<SubTask>(request);

        try
        {
            newTodoItem.Todo = await _db.Todos.SingleAsync(td => td.Id == request.ParentId && td.User.Id == targetUser.Id);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A todo item with id {request.ParentId} doesn't exists", null, HttpStatusCode.NotFound);
        }

        var result = _db.SubTasks.Add(newTodoItem).Entity;
        await _db.SaveChangesAsync();

        return _mapper.Map<SubTaskResponseDto>(result);
    }
}
