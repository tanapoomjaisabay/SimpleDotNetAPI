using AutoMapper;
using BookingAPI.Application.DTOs;
using BookingAPI.Domain.Entities;

namespace BookingAPI.Application.Mappings;

/// <summary>
/// AutoMapper profile for Search History mappings
/// Handles conversions between search criteria and history entities
/// </summary>
public class SearchHistoryMappingProfile : Profile
{
    public SearchHistoryMappingProfile()
    {
        // ====================================================================
        // FlightSearchCriteria to SearchHistory
        // ====================================================================

        /// <summary>
        /// Maps FlightSearchCriteria to SearchHistory for saving search analytics
        /// Custom mapping needed to add TotalFaresFound and SearchedAt
        /// </summary>
        CreateMap<FlightSearchCriteria, SearchHistory>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Will be generated
            .ForMember(dest => dest.TotalFaresFound, opt => opt.Ignore()) // Set manually
            .ForMember(dest => dest.SearchedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CorrelationId, opt => opt.Ignore());
    }
}
