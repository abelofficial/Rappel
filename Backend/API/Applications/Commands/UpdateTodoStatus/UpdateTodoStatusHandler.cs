using System.Net;
using API.Application.Results;
using API.Data;
using API.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Commands;

public class UpdateTodoStatusHandler : BaseHandler<SubTask>, IRequestHandler<UpdateTodoStatusCommand, TodoResponseDto>
{
    private readonly HttpContext _context;
    public UpdateTodoStatusHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<TodoResponseDto> Handle(UpdateTodoStatusCommand request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));

        try
        {
            var todoItem = await _db.Todos.SingleAsync(td => td.User.Id == currentUser.Id && td.Id == request.Id);
            todoItem.Status = request.Status;
            var result = _db.Todos.Update(todoItem).Entity;
            await _db.SaveChangesAsync();
            return _mapper.Map<TodoResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A Todo item with the id {request.Id} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}
