using System.ComponentModel.DataAnnotations;

namespace API.Application.Commands.Dtos;

public class UpdateSubtaskRequestDto
{
    [Required]
    public int ProjectId { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Description { get; set; }

}
