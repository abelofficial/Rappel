using System.Net;

namespace API.Exceptions;

public class ExceptionMessage
{
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}