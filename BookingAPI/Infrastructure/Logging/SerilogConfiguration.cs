using Serilog;
using Serilog.Events;

namespace BookingAPI.Infrastructure.Logging;

/// <summary>
/// Serilog configuration extensions
/// Centralizes all logging configuration following Single Responsibility Principle
/// </summary>
public static class SerilogConfiguration
{
    /// <summary>
    /// Configures Serilog with structured logging to Console and File
    /// </summary>
    public static void ConfigureSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "BookingAPI")
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {CorrelationId} {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(
                path: "logs/booking-api-.log",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] {CorrelationId} {Message:lj}{NewLine}{Exception}",
                retainedFileCountLimit: 30)
            .CreateLogger();
    }

    /// <summary>
    /// Configures Serilog request logging enrichment
    /// </summary>
    public static void ConfigureRequestLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext?.Request?.Host.Value ?? string.Empty);
                diagnosticContext.Set("RequestScheme", httpContext?.Request?.Scheme ?? string.Empty);
                diagnosticContext.Set("RemoteIP", httpContext?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty);
            };
        });
    }
}
