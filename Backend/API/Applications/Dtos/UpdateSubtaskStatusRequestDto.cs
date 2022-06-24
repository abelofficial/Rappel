using System.ComponentModel.DataAnnotations;
using API.Data.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace API.Application.Dtos;

public class UpdateSubtaskStatusRequestDto
{

    [Required]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public ProgressStatus Status { get; set; }
}
