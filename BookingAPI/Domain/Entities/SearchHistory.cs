using BookingAPI.Domain.Enums;

namespace BookingAPI.Domain.Entities;

/// <summary>
/// SearchHistory entity - Tracks flight search analytics
/// Immutable record for audit and analytics purposes
/// </summary>
public record SearchHistory
{
    /// <summary>
    /// Unique identifier for the search history record
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Origin airport code (e.g., "JFK")
    /// </summary>
    public required string Origin { get; init; }

    /// <summary>
    /// Destination airport code (e.g., "LAX")
    /// </summary>
    public required string Destination { get; init; }

    /// <summary>
    /// Requested departure date
    /// </summary>
    public required DateTime DepartureDate { get; init; }

    /// <summary>
    /// Cabin class requested
    /// </summary>
    public required CabinClass CabinClass { get; init; }

    /// <summary>
    /// Number of passengers in the search
    /// </summary>
    public int PassengerCount { get; init; }

    /// <summary>
    /// Total number of flight fares found/returned
    /// </summary>
    public int TotalFaresFound { get; init; }

    /// <summary>
    /// When the search was performed
    /// </summary>
    public DateTime SearchedAt { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Optional user identifier (for future user tracking)
    /// </summary>
    public string? UserId { get; init; }

    /// <summary>
    /// Optional correlation ID for request tracing
    /// </summary>
    public string? CorrelationId { get; init; }
}