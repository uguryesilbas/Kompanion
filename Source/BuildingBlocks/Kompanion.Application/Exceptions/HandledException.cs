using Kompanion.Application.Models;

namespace Kompanion.Application.Exceptions;

public sealed class HandledException : Exception
{
    public HandledException() { }

    public HandledException(int httpStatusCode, string message) : base(message)
    {
        HttpStatusCode = httpStatusCode;
        Errors = new List<ApiResponseErrorMessage> { new(message, string.Empty) };
    }
    public HandledException(Exception exception, int httpStatusCode, string message) : base(message, exception)
    {
        HttpStatusCode = httpStatusCode;
        Errors = new List<ApiResponseErrorMessage> { new(message, string.Empty) };
    }

    public int HttpStatusCode { get; private set; }
    public List<ApiResponseErrorMessage> Errors { get; private set; }
}