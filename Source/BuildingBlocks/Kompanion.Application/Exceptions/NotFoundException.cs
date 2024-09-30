using Microsoft.AspNetCore.Http;
using Kompanion.Application.Models;

namespace Kompanion.Application.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException() { }

    public NotFoundException(string message) : base(message)
    {
        Errors = new List<ApiResponseErrorMessage> { new(message, string.Empty) };
    }

    public NotFoundException(Exception exception, string message) : base(message, exception)
    {
        Errors = new List<ApiResponseErrorMessage> { new(message, string.Empty) };
    }

    public int HttpStatusCode => StatusCodes.Status404NotFound;
    public List<ApiResponseErrorMessage> Errors { get; private set; }
}