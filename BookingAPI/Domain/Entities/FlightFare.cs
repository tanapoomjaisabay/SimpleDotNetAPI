using BookingAPI.Domain.Enums;

namespace BookingAPI.Domain.Entities;

/// <summary>
/// FlightFare as a record - Immutable entity
/// Represents flight fare information with value-based equality
/// </summary>
public record FlightFare
{
    /// <summary>
    /// Unique identifier for the flight fare
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Flight number (e.g., "AA123")
    /// </summary>
    public required string FlightNumber { get; init; }

    /// <summary>
    /// Origin airport code (e.g., "JFK")
    /// </summary>
    public required string Origin { get; init; }

    /// <summary>
    /// Destination airport code (e.g., "LAX")
    /// </summary>
    public required string Destination { get; init; }

    /// <summary>
    /// Base fare amount before taxes
    /// </summary>
    public required decimal BaseFare { get; init; }

    /// <summary>
    /// Tax amount
    /// </summary>
    public required decimal Tax { get; init; }

    /// <summary>
    /// Total fare including base fare and tax
    /// </summary>
    public decimal TotalFare => BaseFare + Tax;

    /// <summary>
    /// Currency code (ISO 4217)
    /// </summary>
    public string Currency { get; init; } = "USD";

    /// <summary>
    /// Departure date and time
    /// </summary>
    public required DateTime DepartureDate { get; init; }

    /// <summary>
    /// Cabin class enum
    /// </summary>
    public required CabinClass CabinClass { get; init; }

    /// <summary>
    /// Creation timestamp (audit field)
    /// </summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
