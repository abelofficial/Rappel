using API.Application.Results;
using MediatR;

namespace API.Application.Queries;


public class GetTodoQuery : IRequest<TodoResponseDto>
{
    public int Id { get; set; }

    public int ProjectId { get; set; }
}