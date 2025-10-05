using BookingAPI.Application.Interfaces;
using BookingAPI.Application.Services;
using BookingAPI.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookingAPI.Application.DependencyInjection;

/// <summary>
/// Application Layer Dependency Injection Configuration
/// Handles all Application layer concerns: Services, Validators, Use Cases, DTOs
/// </summary>
public static class ApplicationServiceExtensions
{
    /// <summary>
    /// Registers all Application layer services
    /// This includes business logic services, validators, and cross-cutting concerns
    /// </summary>
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        // ====================================================================
        // Application Services (Business Logic / Use Cases)
        // ====================================================================
        services.AddScoped<IFlightFareService, FlightFareService>();

        // Future application services
        // services.AddScoped<IBookingService, BookingService>();
        // services.AddScoped<IPaymentService, PaymentService>();
        // services.AddScoped<INotificationService, NotificationService>();

        // ====================================================================
        // FluentValidation (Input Validation)
        // ====================================================================
        services.AddValidatorsFromAssemblyContaining<FlightSearchRequestValidator>();

        // Add FluentValidation to ASP.NET Core pipeline
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

        // ====================================================================
        // AutoMapper (Object-to-Object Mapping)
        // ====================================================================
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // ====================================================================
        // MediatR (if using CQRS pattern in future)
        // ====================================================================
        // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceExtensions).Assembly));

        return services;
    }
}
