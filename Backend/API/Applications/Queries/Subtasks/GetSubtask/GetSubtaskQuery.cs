using API.Application.Results;
using MediatR;

namespace API.Application.Queries;


public class GetSubtaskQuery : IRequest<SubTaskResponseDto>
{
    public int TodoId { get; set; }
    public int SubTaskId { get; set; }

    public int ProjectId { get; set; }
}