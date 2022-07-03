using System.ComponentModel.DataAnnotations;

namespace API.Application.Commands.Dtos;

public class CreateTodoRequestDto
{

    [Required]
    public string? Title { get; set; }
    [Required]
    [MinLength(20)]
    public string? Description { get; set; }
}
