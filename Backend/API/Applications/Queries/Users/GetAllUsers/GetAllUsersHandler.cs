using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetAllUsersHandler : BaseHandler<User>, IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponseDto>>
{

    public GetAllUsersHandler(IMapper mapper, AppDbContext db)
            : base(mapper, db) { }

    public async Task<IEnumerable<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {

        var result = await _db.Users.Where(u => request.Filter == null ? true :
        u.Username.Contains(request.Filter) ||
        u.Username.Contains(request.Filter)
        ).ToListAsync();

        return result.Select(u => _mapper.Map<UserResponseDto>(u));
    }
}