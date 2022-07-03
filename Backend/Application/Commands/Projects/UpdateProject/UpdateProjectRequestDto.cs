using System.ComponentModel.DataAnnotations;

namespace API.Application.Commands.Dtos;

public class UpdateProjectRequestDto
{
    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public bool IsOrdered { get; set; }
}
