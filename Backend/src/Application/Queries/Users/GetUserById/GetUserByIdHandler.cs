using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetUserByIdHandler : BaseHandler<User>, IRequestHandler<GetUserByIdQuery, UserResponseDto>
{

    public GetUserByIdHandler(IMapper mapper, IRepository<User> db)
            : base(mapper, db) { }

    public async Task<UserResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _db.GetOne(request.Id);
            return _mapper.Map<UserResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"User with the id {request.Id} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }
}