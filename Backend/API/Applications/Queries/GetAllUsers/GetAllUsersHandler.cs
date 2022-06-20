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
        var results = await _repo.GetAll();
        return results.Select(u => _mapper.Map<UserResponseDto>(u));
    }
}