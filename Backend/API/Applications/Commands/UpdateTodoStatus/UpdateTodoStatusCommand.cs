using System.ComponentModel.DataAnnotations;
using API.Application.Results;
using API.Data.Entities;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace API.Application.Commands;

public class UpdateTodoStatusCommand : IRequest<TodoResponseDto>
{
    [Required]
    public int? Id { get; set; } = null;

    [Required]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public ProgressStatus Status { get; set; }
}