using API.Application.Results;
using MediatR;

namespace API.Application.Queries;


public class GetAllUserTodoSubtasksQuery : IRequest<IEnumerable<SubTaskResponseDto>>
{
    public int Id { get; set; }
}