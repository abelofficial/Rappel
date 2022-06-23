using API.Application.Results;
using MediatR;

namespace API.Application.Queries;


public class GetUserTodoSubtaskQuery : GetUserTodoQuery
{
    public int SubTaskId { get; set; }
}