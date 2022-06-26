using API.Application.Results;
using MediatR;

namespace API.Application.Queries;


public class GetAllUserTodosQuery : IRequest<IEnumerable<TodoResponseDto>>
{
    public int ProjectId { get; set; }
}