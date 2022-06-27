using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Application.Dtos.CommandsDtos;

public class UpdateProjectRequestDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public bool IsOrdered { get; set; }
}
