using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.Application.Results;
using API.Data.Entities;
using MediatR;
using Newtonsoft.Json;

namespace API.Application.Commands;

public class UpdateSubtaskStatusCommand : IRequest<SubTaskResponseDto>
{
    [Required]
    public int TodoId { get; set; }

    [Required]
    public int SubTaskId { get; set; }

    [Required]
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public ProgressStatus Status { get; set; }
}