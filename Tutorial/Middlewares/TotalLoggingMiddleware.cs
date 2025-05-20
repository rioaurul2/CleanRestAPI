
using System.Diagnostics;

namespace Tutorial.Middlewares;

public class TotalLoggingMiddleware : IMiddleware
{
    private readonly ILogger<TotalLoggingMiddleware> _logger;

    public TotalLoggingMiddleware(ILogger<TotalLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Stopwatch timer = Stopwatch.StartNew();

        await next.Invoke(context);

        timer.Stop();

        _logger.LogInformation("Request {verb} at {Path} took {time} ms"
            , context.Request.Method
            , context.Request.Path
            , timer.ElapsedMilliseconds);
    }
}
