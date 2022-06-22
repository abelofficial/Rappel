using System.Net;
using Newtonsoft.Json;

namespace API.Exceptions;

public class ExceptionMessage
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("status")]
    public HttpStatusCode Status { get; set; }

    [JsonProperty("errors")]
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}