using System.ComponentModel.DataAnnotations;
using API.Application.Results;
using MediatR;

namespace API.Application.Commands;

public class CreateTodoCommand : IRequest<TodoResponseDto>
{
    [Required]
    public string Title { get; set; }
    [Required]
    [MinLength(20)]
    public string Description { get; set; }
}