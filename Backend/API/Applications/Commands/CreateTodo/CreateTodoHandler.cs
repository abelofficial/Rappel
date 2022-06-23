using API.Application.Results;
using API.Data;
using API.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Commands;

public class CreateTodoHandler : BaseHandler<Todo>, IRequestHandler<CreateTodoCommand, TodoResponseDto>
{
    private readonly HttpContext _context;

    public CreateTodoHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<TodoResponseDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var targetUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));

        var newTodoItem = _mapper.Map<Todo>(request);
        newTodoItem.AddOwner(targetUser);
        var result = _db.Todos.Add(newTodoItem).Entity;
        await _db.SaveChangesAsync();

        return _mapper.Map<TodoResponseDto>(result);
    }
}
