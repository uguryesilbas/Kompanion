using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Kompanion.Application.Wrappers;

namespace Kompanion.Application.MediatR.Behaviors;

internal sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest where TResponse : ApiResponse
{
    private const string RequestName = nameof(RequestName);

    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        using (LogContext.PushProperty(RequestName, typeof(TRequest).Name))
        {
            try
            {
                _logger.LogInformation("[START] Request => {@Body}", request);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Request log insert fail!");
            }

            TResponse response = await next();

            try
            {
                _logger.LogInformation("[END] Execution Time: {execution}ms,  Response => {@Body}", stopwatch.ElapsedMilliseconds, response);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Response log insert fail!");
            }

            return response;
        }
    }
}