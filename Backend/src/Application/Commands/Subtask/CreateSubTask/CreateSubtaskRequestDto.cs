using System.ComponentModel.DataAnnotations;

namespace API.Application.Commands.Dtos;

public class CreateSubtaskRequestDto
{

    [Required]
    public string? Title { get; set; }
    [Required]
    public int ProjectId { get; set; }
    [Required]
    [MinLength(20)]
    public string? Description { get; set; }
}
