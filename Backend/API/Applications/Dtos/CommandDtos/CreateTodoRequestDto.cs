using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Application.Dtos.CommandsDtos;

public class CreateTodoRequestDto
{

    [Required]
    public string Title { get; set; }
    [Required]
    [MinLength(20)]
    public string Description { get; set; }
}
