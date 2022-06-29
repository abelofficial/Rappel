using API.Application.Results;
using MediatR;

namespace API.Application.Queries;


public class GetUserByIdQuery : IRequest<UserResponseDto>
{
    public int Id { get; set; }
}