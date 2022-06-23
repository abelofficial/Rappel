using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.Application.Results;
using API.Data.Entities;
using MediatR;
using Newtonsoft.Json;

namespace API.Application.Commands;

public class UpdateTodoStatusCommand : IRequest<TodoResponseDto>
{
    [Required]
    public int? Id { get; set; } = null;

    [Required]
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public ProgressStatus Status { get; set; }
}