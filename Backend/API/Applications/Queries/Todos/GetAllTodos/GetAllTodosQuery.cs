using API.Application.Results;
using MediatR;

namespace API.Application.Queries;


public class GetAllTodosQuery : IRequest<IEnumerable<TodoResponseDto>>
{
    public int ProjectId { get; set; }
}