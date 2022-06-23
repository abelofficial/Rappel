using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.Application.Results;
using API.Data.Entities;
using MediatR;
using Newtonsoft.Json;

namespace API.Application.Dtos;

public class UpdateSubtaskStatusRequestDto
{

    [Required]
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public ProgressStatus Status { get; set; }
}
