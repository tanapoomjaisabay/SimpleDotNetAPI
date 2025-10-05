namespace BookingAPI.Domain.Enums;

/// <summary>
/// Cabin class types for flight bookings
/// Represents the different seating categories available on flights
/// </summary>
public enum CabinClass
{
    /// <summary>
    /// Economy class - Standard seating
    /// </summary>
    Economy = 0,

    /// <summary>
    /// Premium Economy - Enhanced economy with extra legroom
    /// </summary>
    PremiumEconomy = 1,

    /// <summary>
    /// Business class - Premium seating with enhanced services
    /// </summary>
    Business = 2,

    /// <summary>
    /// First class - Highest tier with luxury amenities
    /// </summary>
    First = 3
}
