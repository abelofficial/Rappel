using API.Application.Results;
using MediatR;

namespace API.Application.Queries;


public class GetProjectQuery : IRequest<ProjectResponseDto>
{
    public int Id { get; set; }
}