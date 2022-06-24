using System.Text.Json.Serialization;
using API.Data.Entities;

namespace API.Application.Results;

public class TodoResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public ProgressStatus Status { get; set; }

    public UserResponseDto User { get; set; }

}
