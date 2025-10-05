using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace BookingAPI.Infrastructure.Middleware;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string CorrelationIdHeaderName = "X-Correlation-ID";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Get or generate correlation ID
        var correlationId = context.Request.Headers[CorrelationIdHeaderName].FirstOrDefault()
                           ?? Guid.NewGuid().ToString();

        // Add to response headers
        context.Response.Headers.Append(CorrelationIdHeaderName, correlationId);

        // Push to Serilog LogContext
        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await _next(context);
        }
    }
}
