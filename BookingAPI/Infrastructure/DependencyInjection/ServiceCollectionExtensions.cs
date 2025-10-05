using BookingAPI.Domain.Interfaces;
using BookingAPI.Infrastructure.DataAccess;
using BookingAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookingAPI.Infrastructure.DependencyInjection;

/// <summary>
/// Infrastructure Layer Dependency Injection Configuration
/// Handles all Infrastructure concerns: Database, External APIs, File System, etc.
/// </summary>
public static class InfrastructureServiceExtensions
{
    /// <summary>
    /// Registers all Infrastructure layer services
    /// This includes repositories, database context, external service clients, etc.
    /// </summary>
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        // ====================================================================
        // Database Context (Mock SQL Server with In-Memory Database)
        // ====================================================================
        // To switch to real SQL Server:
        // options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        services.AddDbContext<BookingDbContext>(options =>
            options.UseInMemoryDatabase("BookingApiDb")
                   .EnableSensitiveDataLogging()  // Development only
                   .EnableDetailedErrors());       // Development only

        // ====================================================================
        // Repositories (Data Access)
        // ====================================================================
        services.AddScoped<IFlightFareRepository, FlightFareRepository>();
        services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();

        // ====================================================================
        // External Service Clients (Future)
        // ====================================================================
        // services.AddHttpClient<IPaymentGateway, StripePaymentGateway>();
        // services.AddScoped<IEmailService, SendGridEmailService>();
        // services.AddScoped<ISmsService, TwilioSmsService>();

        // ====================================================================
        // Caching (Future)
        // ====================================================================
        // services.AddMemoryCache();
        // services.AddDistributedMemoryCache();
        // services.AddStackExchangeRedisCache(options => ...);

        // ====================================================================
        // Message Queue / Service Bus (Future)
        // ====================================================================
        // services.AddSingleton<IMessageBus, RabbitMQMessageBus>();
        // services.AddSingleton<IEventBus, AzureServiceBus>();

        return services;
    }
}