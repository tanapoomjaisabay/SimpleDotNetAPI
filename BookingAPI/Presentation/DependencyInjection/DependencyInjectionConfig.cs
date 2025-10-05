using System.Text.Json;
using BookingAPI.Application.DependencyInjection;
using BookingAPI.Infrastructure.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BookingAPI.Presentation.DependencyInjection;

/// <summary>
/// Presentation Layer Dependency Injection Configuration
/// Handles all Presentation layer concerns: Controllers, Swagger, API configuration
/// </summary>
public static class PresentationServiceExtensions
{
    /// <summary>
    /// Registers all Presentation layer services and dependencies
    /// This is the Composition Root that orchestrates all layers
    /// </summary>
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        // ====================================================================
        // Presentation Layer - Controllers & API Configuration
        // ====================================================================
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                // ============================================================
                // JSON Serialization Configuration (Modern ASP.NET Core way)
                // This replaces the old JsonMediaTypeFormatter approach
                // ============================================================

                // Property Naming Strategy
                // options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // camelCase (recommended for JavaScript clients)
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // PascalCase (C# convention)

                // Formatting
                options.JsonSerializerOptions.WriteIndented = false; // Compact JSON (production)
                                                                     // options.JsonSerializerOptions.WriteIndented = true; // Pretty JSON (development)

                // Null Handling
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
                // options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // Omit null properties

                // Enum Handling
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

                // Number Handling
                options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict;

                // Reference Handling (for circular references)
                // options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

                // Property Name Case Insensitive (when deserializing)
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

                // Allow trailing commas in JSON
                options.JsonSerializerOptions.AllowTrailingCommas = true;

                // Read comments in JSON
                options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
            });

        // Health Checks
        services.AddHealthChecks();

        // OpenAPI/Swagger
        services.AddOpenApi();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Booking API",
                Description = "An ASP.NET Core Web API for Flight Booking and Fare Search with Clean Architecture",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Development Team",
                    Email = "dev@example.com",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            // Add XML comments if available
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }

            // Add Correlation ID header to Swagger
            options.AddSecurityDefinition("CorrelationId", new OpenApiSecurityScheme
            {
                Description = "Correlation ID for distributed request tracking across services",
                Name = "X-Correlation-ID",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "CorrelationId"
            });

            // Enable annotations for better documentation
            options.EnableAnnotations();
        });

        // Future Presentation-specific services
        // services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }

    /// <summary>
    /// Configures Presentation layer middleware pipeline
    /// </summary>
    public static WebApplication UsePresentationLayer(this WebApplication app)
    {
        // Swagger UI (non-production only)
        if (!app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking API v1");
                options.RoutePrefix = "swagger";
                options.DocumentTitle = "Booking API Documentation";
                options.DisplayRequestDuration();
                options.EnableDeepLinking();
                options.EnableFilter();
                options.ShowExtensions();
            });

            app.MapOpenApi();
        }

        return app;
    }
}
