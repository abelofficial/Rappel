using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class CurrentUserHandler : BaseHandler<User>, IRequestHandler<CurrentUserQuery, UserResponseDto>
{
    public readonly HttpContext _context;
    public CurrentUserHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<UserResponseDto> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));
        return _mapper.Map<UserResponseDto>(currentUser);
    }
}