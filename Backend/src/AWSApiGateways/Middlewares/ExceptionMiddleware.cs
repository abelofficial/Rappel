using System.Net;
using System.Text.Json;
using API.Domain.Exceptions;

namespace AWSApiGateways.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpRequestException error)
        {
            var response = context.Response;

            response.ContentType = "application/json";
            var errorStatus = (error.StatusCode is not null) ? (HttpStatusCode)error.StatusCode : HttpStatusCode.InternalServerError;
            response.StatusCode = (int)errorStatus;

            var result = JsonSerializer.Serialize(
                new ExceptionMessage()
                {
                    Title = HttpStatusCode.GetName(errorStatus),
                    Status = errorStatus,
                    Errors = new List<string>(error.Message.Split("\n")) { },
                });
            await response.WriteAsync(result);
        }
        catch (Exception error)
        {
            Console.WriteLine("ERROR: " + error);
            var response = context.Response;

            response.ContentType = "application/json";
            var errorStatus = HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(
                new ExceptionMessage()
                {
                    Title = HttpStatusCode.GetName(errorStatus),
                    Status = errorStatus,
                    Errors = new List<string>(error.Message.Split("\n")) { },
                });
            await response.WriteAsync(result);
        }
    }
}