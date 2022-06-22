using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class CurrentUserHandler : BaseHandler<User>, IRequestHandler<CurrentUserQuery, UserResponseDto>
{
    public readonly HttpContext _context;
    public CurrentUserHandler(IMapper mapper, IRepository<User> repo, IHttpContextAccessor httpContextAccessor)
            : base(mapper, repo)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<UserResponseDto> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = await _repo.GetAll(u => u.Username.Equals(currentUserName));
        return _mapper.Map<UserResponseDto>(currentUser.Single());
    }
}