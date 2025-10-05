using Serilog;
using BookingAPI.Infrastructure.Logging;
using BookingAPI.Infrastructure.Middleware;
using BookingAPI.Presentation.DependencyInjection;
using BookingAPI.Application.DependencyInjection;
using BookingAPI.Infrastructure.DependencyInjection;

// ============================================================================
// Configure Serilog - Structured Logging
// ============================================================================
SerilogConfiguration.ConfigureSerilog();

try
{
    Log.Information("Starting BookingAPI application");

    // ========================================================================
    // Build Application
    // ========================================================================
    var builder = WebApplication.CreateBuilder(args);

    // Add Serilog to application
    builder.Host.UseSerilog();

    // ========================================================================
    // Register All Layers (Dependency Injection)
    // Each layer handles its own complete configuration:
    // - Presentation: Controllers, Swagger, API settings
    // - Application: Services, Validators, Business Logic
    // - Infrastructure: Database, Repositories, External Services
    // ========================================================================
    builder.Services.AddPresentationLayer();
    builder.Services.AddApplicationLayer();
    builder.Services.AddInfrastructureLayer();

    // ========================================================================
    // Build and Configure Application Pipeline
    // ========================================================================
    var app = builder.Build();

    // ====================================================================
    // Middleware Pipeline (Order Matters!)
    // ====================================================================

    // 1. Correlation ID (first - tracks entire request)
    app.UseMiddleware<CorrelationIdMiddleware>();

    // 2. Serilog request logging
    app.ConfigureRequestLogging();

    // 3. HTTPS Redirection (production only)
    if (app.Environment.IsProduction())
    {
        app.UseHttpsRedirection();
    }

    // 4. Routing
    app.UseRouting();

    // 5. Authentication (future)
    // app.UseAuthentication();

    // 6. Authorization
    app.UseAuthorization();

    // 7. Presentation layer middleware (Swagger, etc.)
    app.UsePresentationLayer();

    // 8. Map endpoints
    app.MapControllers();
    app.MapHealthChecks("/healthz");

    // ========================================================================
    // Start Application
    // ========================================================================
    Log.Information("BookingAPI application started successfully on {Environment}",
        app.Environment.EnvironmentName);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    throw;
}
finally
{
    Log.Information("Shutting down BookingAPI application");
    Log.CloseAndFlush();
}
