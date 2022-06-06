using System.Net;

namespace Users.Api.Common.ErrorHandler;

public record ErrorDetails()
{
    public HttpStatusCode Status { get; init; } = HttpStatusCode.InternalServerError;
    public string Message { get; init; } = "An unknown error occurred";
    public IDictionary<string, string>? Errors { get; init; } = new Dictionary<string, string>();
}