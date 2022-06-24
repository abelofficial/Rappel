using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.Data.Entities;
using Newtonsoft.Json;

namespace API.Application.Dtos;

public class UpdateTodoStatusRequestDto
{

    [Required]
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public ProgressStatus Status { get; set; }
}
