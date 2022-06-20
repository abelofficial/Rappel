using API.Application.Results;
using MediatR;

namespace API.Application.Queries;


public class GetAllUsersQuery : IRequest<IEnumerable<UserResponseDto>>
{ }