using AutoMapper;
using BookingAPI.Application.DTOs;
using BookingAPI.Application.Interfaces;
using BookingAPI.Domain.Entities;
using BookingAPI.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace BookingAPI.Application.Services;

public class FlightFareService : IFlightFareService
{
    private readonly IFlightFareRepository _repository;
    private readonly ISearchHistoryRepository _searchHistoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<FlightFareService> _logger;

    public FlightFareService(
        IFlightFareRepository repository,
        ISearchHistoryRepository searchHistoryRepository,
        IMapper mapper,
        ILogger<FlightFareService> logger)
    {
        _repository = repository;
        _searchHistoryRepository = searchHistoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<FlightFareResponseDto>> SearchFlightFaresAsync(FlightSearchRequestDto request)
    {
        _logger.LogInformation(
            "Starting flight fare search - Origin: {Origin}, Destination: {Destination}, Date: {DepartureDate}, Class: {CabinClass}",
            request.Origin, request.Destination, request.DepartureDate, request.CabinClass);

        try
        {
            // Map DTO to Domain using AutoMapper
            var criteria = _mapper.Map<FlightSearchCriteria>(request);

            _logger.LogInformation("Processing flight fare search with criteria mapped");

            // Call repository to get flight fares
            var flightFares = await _repository.SearchFlightFaresAsync(criteria);

            _logger.LogInformation("Flight fare search completed - Found {Count} flights", flightFares.Count());

            // Save search history to database (mock SQL Server)
            try
            {
                // Map criteria to SearchHistory using AutoMapper, then set additional fields
                var searchHistory = _mapper.Map<SearchHistory>(criteria);
                searchHistory = searchHistory with
                {
                    TotalFaresFound = flightFares.Count(),
                    CorrelationId = GetCorrelationId()
                };

                await _searchHistoryRepository.SaveSearchHistoryAsync(searchHistory);

                _logger.LogInformation(
                    "Search history saved - ID: {HistoryId}, Total Fares: {Count}",
                    searchHistory.Id,
                    searchHistory.TotalFaresFound);
            }
            catch (Exception historyEx)
            {
                // Log error but don't fail the search operation
                _logger.LogError(historyEx, "Failed to save search history, but continuing with search results");
            }

            // Map Domain Entities to Response DTOs using AutoMapper
            var response = _mapper.Map<IEnumerable<FlightFareResponseDto>>(flightFares);

            _logger.LogInformation("Ending flight fare search - Successfully returned {Count} results", response.Count());

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during flight fare search");
            throw;
        }
    }

    /// <summary>
    /// Gets correlation ID from current context (placeholder for future implementation)
    /// In production, this would get the correlation ID from HttpContext or logging context
    /// </summary>
    private string? GetCorrelationId()
    {
        // TODO: Implement actual correlation ID retrieval from HttpContext or Serilog context
        // For now, return null or generate a new one
        return null;
    }
}
