using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetAllUsersHandler : BaseHandler<User>, IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponseDto>>
{

    public GetAllUsersHandler(IMapper mapper, IRepository<User> repo)
            : base(mapper, repo) { }

    public async Task<IEnumerable<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {

        var result = await _repo.GetAll(u => request.Filter == null ? true :
        u.Username.Contains(request.Filter) ||
        u.Username.Contains(request.Filter)
        );

        return result.Select(u => _mapper.Map<UserResponseDto>(u));
    }
}