using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace API.Application.Queries;

public class CurrentUserHandler : BaseHandler<User>, IRequestHandler<CurrentUserQuery, UserResponseDto>
{
    public readonly HttpContext _context;
    public CurrentUserHandler(IMapper mapper, IRepository<User> db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<UserResponseDto> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = await _db.GetOne(u => u.Username.Equals(currentUserName));
        return _mapper.Map<UserResponseDto>(currentUser);
    }
}