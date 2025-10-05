using AutoMapper;
using BookingAPI.Application.DTOs;
using BookingAPI.Domain.Entities;

namespace BookingAPI.Application.Mappings;

/// <summary>
/// AutoMapper profile for Flight Fare mappings
/// Handles conversions between Domain Entities and DTOs
/// </summary>
public class FlightFareMappingProfile : Profile
{
    public FlightFareMappingProfile()
    {
        // ====================================================================
        // Request DTO to Domain Entity Mappings
        // ====================================================================

        /// <summary>
        /// Maps FlightSearchRequestDto to FlightSearchCriteria
        /// Used when converting incoming API requests to domain search criteria
        /// </summary>
        CreateMap<FlightSearchRequestDto, FlightSearchCriteria>();

        // ====================================================================
        // Domain Entity to Response DTO Mappings
        // ====================================================================

        /// <summary>
        /// Maps FlightFare domain entity to FlightFareResponseDto
        /// Used when returning flight fare results to API clients
        /// </summary>
        CreateMap<FlightFare, FlightFareResponseDto>();

        // Alternative with explicit mapping (if property names differ):
        // CreateMap<FlightFare, FlightFareResponseDto>()
        //     .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.FlightNumber))
        //     .ForMember(dest => dest.TotalFare, opt => opt.MapFrom(src => src.BaseFare + src.Tax));
    }
}
