namespace BookingsWithMe.Middleware;

public class GlobalErrorHandler : IMiddleware
{
    private readonly ILogger<GlobalErrorHandler> _logger;
    public GlobalErrorHandler(ILogger<GlobalErrorHandler> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception occurred with context {context} and exception {ex}", context, ex);
            context.Response.StatusCode = 500;
        }
    }
}
