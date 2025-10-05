using BookingAPI.Domain.Enums;

namespace BookingAPI.Application.DTOs;

/// <summary>
/// Flight search request DTO - Immutable record
/// Validation is handled by FluentValidation in Application layer
/// </summary>
public record FlightSearchRequestDto
{
    public required string Origin { get; init; }
    public required string Destination { get; init; }
    public required DateTime DepartureDate { get; init; }
    public CabinClass CabinClass { get; init; } = CabinClass.Economy;
    public int PassengerCount { get; init; } = 1;
}
