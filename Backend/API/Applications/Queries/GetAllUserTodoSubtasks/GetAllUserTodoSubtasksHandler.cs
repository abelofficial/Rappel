using API.Application.Results;
using API.Data;
using API.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetAllUserTodoSubtasksHandler : BaseHandler<SubTask>, IRequestHandler<GetAllUserTodoSubtasksQuery, IEnumerable<SubTaskResponseDto>>
{
    private readonly HttpContext _context;

    public GetAllUserTodoSubtasksHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;

    }

    public async Task<IEnumerable<SubTaskResponseDto>> Handle(GetAllUserTodoSubtasksQuery request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));

        var result = await _db.SubTasks.Where(u => u.Todo.User.Id == currentUser.Id).Where(st => st.Todo.Id == request.Id).ToListAsync();
        return _mapper.Map<IEnumerable<SubTaskResponseDto>>(result);
    }
}