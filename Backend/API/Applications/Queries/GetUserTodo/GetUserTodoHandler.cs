using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetUserTodoHandler : BaseHandler<Todo>, IRequestHandler<GetUserTodoQuery, TodoResponseDto>
{

    private readonly HttpContext _context;

    public GetUserTodoHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<TodoResponseDto> Handle(GetUserTodoQuery request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));

        try
        {
            var result = await _db.Todos
            .Include(td => td.SubTask)
            .Include(td => td.User)
            .SingleAsync(td => td.Project.Members.Any(m => m.Id == currentUser.Id) && td.Id == request.Id && td.Project.Id == request.ProjectId);

            return _mapper.Map<TodoResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"A Todo item with the id {request.Id} in project ${request.ProjectId} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}