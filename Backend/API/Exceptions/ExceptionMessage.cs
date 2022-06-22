using System.Net;
using System.Text.Json.Serialization;

namespace API.Exceptions;

public class ExceptionMessage
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("status")]
    public HttpStatusCode Status { get; set; }

    [JsonPropertyName("errors")]
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}