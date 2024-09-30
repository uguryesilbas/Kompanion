using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Kompanion.Application.Wrappers;

namespace Kompanion.Application.MediatR.Behaviors;

internal class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IBaseRequest
    where TResponse : notnull, ApiResponse
{
    private const string RequestName = nameof(RequestName);
    private const int MaximumMilliseconds = 500;

    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehavior(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        TResponse response = await next();

        _timer.Stop();

        long elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds <= MaximumMilliseconds)
        {
            return response;
        }

        using (LogContext.PushProperty(RequestName, typeof(TRequest).Name))
        {
            _logger.LogWarning("Long Running Request ({ElapsedMilliseconds} milliseconds) {@Request}", elapsedMilliseconds, request);
        }

        return response;
    }
}