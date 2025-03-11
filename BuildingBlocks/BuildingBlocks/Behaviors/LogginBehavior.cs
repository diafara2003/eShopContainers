using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LogginBehavior<TRequest, TResponse>
    (ILogger<LogginBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] handle request={Request} - Response={Response}",
            typeof(TRequest).Name,
            typeof(TResponse).Name);

        var timer = Stopwatch.StartNew();
        timer.Start();

        var response = await next();
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
            logger.LogWarning("[PERFORMANCE] the request={Request} - took={TimeTaken}",
                typeof(TRequest).Name,
                timeTaken);

        logger.LogInformation("[END] handle request={Request} - Response={Response}",
            typeof(TRequest).Name,
            typeof(TResponse).Name);

        return response;
    }
}
