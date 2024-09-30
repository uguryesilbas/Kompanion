using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Kompanion.Application.Wrappers;

namespace Kompanion.Application.MediatR.Behaviors;

internal sealed class ExceptionBehavior<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TRequest : notnull, IBaseRequest 
    where TResponse : notnull, ApiResponse 
    where TException : Exception
{
    private readonly ILogger<ExceptionBehavior<TRequest, TResponse, TException>> _logger;

    public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse, TException>> logger)
    {
        _logger = logger;
    }

    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogError(exception, "Request: Unhandled Exception for Request {Title} {@Request}", requestName, request);

        throw exception;
    }
}