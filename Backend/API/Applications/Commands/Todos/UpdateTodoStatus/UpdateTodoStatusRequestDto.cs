using System.ComponentModel.DataAnnotations;
using API.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace API.Application.Commands.Dtos;

public class UpdateTodoStatusRequestDto
{

    [Required]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public ProgressStatus Status { get; set; }
}
