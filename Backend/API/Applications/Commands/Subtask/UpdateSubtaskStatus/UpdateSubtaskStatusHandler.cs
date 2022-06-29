using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Commands;

public class UpdateSubtaskStatusHandler : BaseHandler<SubTask>, IRequestHandler<UpdateSubtaskStatusCommand, SubTaskResponseDto>
{
    private readonly HttpContext _context;
    public UpdateSubtaskStatusHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<SubTaskResponseDto> Handle(UpdateSubtaskStatusCommand request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));

        try
        {
            var todoItem = await _db.SubTasks.Where(st => st.Todo.User.Id == currentUser.Id).Where(st => st.Todo.Id == request.TodoId).SingleAsync(st => st.Id == request.SubTaskId);
            todoItem.Status = request.Status;
            var result = _db.SubTasks.Update(todoItem).Entity;
            await _db.SaveChangesAsync();
            return _mapper.Map<SubTaskResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A Subtask item with the id {request.SubTaskId} for the todo item id {request.TodoId} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}
