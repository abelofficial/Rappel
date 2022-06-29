using System.ComponentModel.DataAnnotations;
using API.Application.Results;
using API.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace API.Application.Commands;

public class UpdateSubtaskStatusCommand : IRequest<SubTaskResponseDto>
{
    [Required]
    public int TodoId { get; set; }

    [Required]
    public int SubTaskId { get; set; }

    [Required]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public ProgressStatus Status { get; set; }
}