using BookingAPI.Domain.Entities;

namespace BookingAPI.Domain.Interfaces;

public interface IFlightFareRepository
{
    Task<IEnumerable<FlightFare>> SearchFlightFaresAsync(FlightSearchCriteria criteria);
}
