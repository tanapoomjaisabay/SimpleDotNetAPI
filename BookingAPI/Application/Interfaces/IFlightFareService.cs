using BookingAPI.Application.DTOs;

namespace BookingAPI.Application.Interfaces;

public interface IFlightFareService
{
    Task<IEnumerable<FlightFareResponseDto>> SearchFlightFaresAsync(FlightSearchRequestDto request);
}
