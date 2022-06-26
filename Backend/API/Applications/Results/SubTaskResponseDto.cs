using API.Data.Entities;
using Newtonsoft.Json.Converters;

namespace API.Application.Results;

public class SubTaskResponseDto
{
    public int Id { get; set; }


    public string Title { get; set; }

    public string Description { get; set; }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public ProgressStatus Status { get; set; }

    public int TodoId { get; set; }
}

