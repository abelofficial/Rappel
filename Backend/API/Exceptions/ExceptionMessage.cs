using System.Net;

namespace API.Exceptions;

public class ExceptionMessage
{
    public string Title { get; set; }
    public HttpStatusCode status { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}