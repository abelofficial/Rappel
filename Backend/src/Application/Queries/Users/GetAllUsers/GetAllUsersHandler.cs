using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetAllUsersHandler : BaseHandler<User>, IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponseDto>>
{

    public GetAllUsersHandler(IMapper mapper, IRepository<User> db)
            : base(mapper, db) { }

    public async Task<IEnumerable<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {

        var result = await _db.GetAll(u => request.Filter == null ? true :
        u.Username.Contains(request.Filter) ||
        u.Username.Contains(request.Filter)
        );

        return result.Select(u => _mapper.Map<UserResponseDto>(u));
    }
}