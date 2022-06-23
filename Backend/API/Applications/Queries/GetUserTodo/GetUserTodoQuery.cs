using API.Application.Results;
using MediatR;

namespace API.Application.Queries;


public class GetUserTodoQuery : IRequest<TodoResponseDto>
{
    public int Id { get; set; }
}