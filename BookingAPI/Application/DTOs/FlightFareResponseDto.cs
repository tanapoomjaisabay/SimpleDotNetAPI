using BookingAPI.Domain.Enums;

namespace BookingAPI.Application.DTOs;

/// <summary>
/// Flight fare response DTO - Perfect for record!
/// Immutable value object for API responses
/// </summary>
public record FlightFareResponseDto(
    string FlightNumber,
    string Origin,
    string Destination,
    decimal BaseFare,
    decimal Tax,
    decimal TotalFare,
    string Currency,
    DateTime DepartureDate,
    CabinClass CabinClass
);
