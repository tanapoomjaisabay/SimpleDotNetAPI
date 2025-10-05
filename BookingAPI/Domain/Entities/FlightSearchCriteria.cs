using BookingAPI.Domain.Enums;

namespace BookingAPI.Domain.Entities;

/// <summary>
/// FlightSearchCriteria as a record - Value Object
/// - Immutable
/// - No identity
/// - Value-based equality
/// </summary>
public record FlightSearchCriteria
{
    /// <summary>
    /// Origin airport code (e.g., "JFK")
    /// </summary>
    public required string Origin { get; init; }

    /// <summary>
    /// Destination airport code (e.g., "LAX")
    /// </summary>
    public required string Destination { get; init; }

    /// <summary>
    /// Departure date
    /// </summary>
    public required DateTime DepartureDate { get; init; }

    /// <summary>
    /// Cabin class preference
    /// </summary>
    public CabinClass CabinClass { get; init; } = CabinClass.Economy;

    /// <summary>
    /// Number of passengers
    /// </summary>
    public int PassengerCount { get; init; } = 1;
}
