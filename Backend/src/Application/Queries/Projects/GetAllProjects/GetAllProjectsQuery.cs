using API.Application.Results;
using MediatR;

namespace API.Application.Queries;


public class GetAllProjectsQuery : IRequest<IEnumerable<ProjectResponseDto>>
{
}