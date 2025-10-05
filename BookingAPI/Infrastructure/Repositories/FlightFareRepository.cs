using BookingAPI.Domain.Entities;
using BookingAPI.Domain.Enums;
using BookingAPI.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace BookingAPI.Infrastructure.Repositories;

public class FlightFareRepository : IFlightFareRepository
{
    private readonly ILogger<FlightFareRepository> _logger;

    public FlightFareRepository(ILogger<FlightFareRepository> logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<FlightFare>> SearchFlightFaresAsync(FlightSearchCriteria criteria)
    {
        _logger.LogInformation(
            "Repository: Starting database search for flights from {Origin} to {Destination}",
            criteria.Origin, criteria.Destination);

        // Simulate database delay
        await Task.Delay(500);

        _logger.LogInformation("Repository: Processing flight fare calculation");

        // Dummy data - simulating database results
        var flightFares = new List<FlightFare>
        {
            new FlightFare
            {
                FlightNumber = "BA101",
                Origin = criteria.Origin,
                Destination = criteria.Destination,
                BaseFare = 450.00m,
                Tax = 75.50m,
                Currency = "USD",
                DepartureDate = criteria.DepartureDate,
                CabinClass = criteria.CabinClass
            },
            new FlightFare
            {
                FlightNumber = "UA205",
                Origin = criteria.Origin,
                Destination = criteria.Destination,
                BaseFare = 425.00m,
                Tax = 68.75m,
                Currency = "USD",
                DepartureDate = criteria.DepartureDate,
                CabinClass = criteria.CabinClass
            },
            new FlightFare
            {
                FlightNumber = "AA330",
                Origin = criteria.Origin,
                Destination = criteria.Destination,
                BaseFare = 475.00m,
                Tax = 82.25m,
                Currency = "USD",
                DepartureDate = criteria.DepartureDate,
                CabinClass = criteria.CabinClass
            }
        };

        // Apply cabin class pricing multiplier using enum
        var multiplier = criteria.CabinClass switch
        {
            CabinClass.Economy => 1.0m,
            CabinClass.PremiumEconomy => 1.5m,
            CabinClass.Business => 2.5m,
            CabinClass.First => 4.0m,
            _ => 1.0m
        };

        // Since FlightFare is now immutable (record with init), create new instances with adjusted prices
        var adjustedFlightFares = flightFares.Select(fare => fare with
        {
            BaseFare = fare.BaseFare * multiplier,
            Tax = fare.Tax * multiplier
        }).ToList();

        _logger.LogInformation(
            "Repository: Ending database search - Retrieved {Count} flight fares",
            adjustedFlightFares.Count);

        return adjustedFlightFares;
    }
}
