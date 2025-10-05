using BookingAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Presentation.Controllers;

/// <summary>
/// Analytics Controller - Provides search analytics and history
/// Demonstrates the search history feature with mock database
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly ISearchHistoryRepository _searchHistoryRepository;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(
        ISearchHistoryRepository searchHistoryRepository,
        ILogger<AnalyticsController> logger)
    {
        _searchHistoryRepository = searchHistoryRepository;
        _logger = logger;
    }

    /// <summary>
    /// Get recent search history
    /// </summary>
    /// <param name="limit">Number of recent searches to return (default: 10)</param>
    /// <returns>List of recent searches</returns>
    [HttpGet("recent-searches")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRecentSearches([FromQuery] int limit = 10)
    {
        try
        {
            _logger.LogInformation("Retrieving {Limit} recent searches", limit);

            var recentSearches = await _searchHistoryRepository.GetRecentSearchesAsync(limit);

            return Ok(new
            {
                totalCount = recentSearches.Count(),
                searches = recentSearches.Select(s => new
                {
                    id = s.Id,
                    route = $"{s.Origin} → {s.Destination}",
                    origin = s.Origin,
                    destination = s.Destination,
                    departureDate = s.DepartureDate.ToString("yyyy-MM-dd"),
                    cabinClass = s.CabinClass,
                    passengerCount = s.PassengerCount,
                    totalFaresFound = s.TotalFaresFound,
                    searchedAt = s.SearchedAt.ToString("yyyy-MM-dd HH:mm:ss")
                })
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving recent searches");
            return StatusCode(500, new { error = "An error occurred while retrieving recent searches" });
        }
    }

    /// <summary>
    /// Get popular routes based on search frequency
    /// </summary>
    /// <param name="topCount">Number of top routes to return (default: 10)</param>
    /// <returns>List of popular routes with search counts</returns>
    [HttpGet("popular-routes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPopularRoutes([FromQuery] int topCount = 10)
    {
        try
        {
            _logger.LogInformation("Retrieving top {Count} popular routes", topCount);

            var popularRoutes = await _searchHistoryRepository.GetPopularRoutesAsync(topCount);

            return Ok(new
            {
                totalRoutes = popularRoutes.Count(),
                routes = popularRoutes.Select(r => new
                {
                    route = $"{r.Origin} → {r.Destination}",
                    origin = r.Origin,
                    destination = r.Destination,
                    searchCount = r.SearchCount
                })
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving popular routes");
            return StatusCode(500, new { error = "An error occurred while retrieving popular routes" });
        }
    }

    /// <summary>
    /// Get search history statistics
    /// </summary>
    /// <returns>Statistics about search history</returns>
    [HttpGet("statistics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStatistics()
    {
        try
        {
            _logger.LogInformation("Retrieving search statistics");

            var allSearches = await _searchHistoryRepository.GetRecentSearchesAsync(1000); // Get more for stats
            var popularRoutes = await _searchHistoryRepository.GetPopularRoutesAsync(5);

            var stats = new
            {
                totalSearches = allSearches.Count(),
                totalFaresReturned = allSearches.Sum(s => s.TotalFaresFound),
                averageFaresPerSearch = allSearches.Any() ? allSearches.Average(s => s.TotalFaresFound) : 0,
                mostPopularCabinClass = allSearches.GroupBy(s => s.CabinClass)
                    .OrderByDescending(g => g.Count())
                    .Select(g => new { cabinClass = g.Key, count = g.Count() })
                    .FirstOrDefault(),
                topRoutes = popularRoutes.Take(5).Select(r => new
                {
                    route = $"{r.Origin} → {r.Destination}",
                    count = r.SearchCount
                }),
                searchesByDate = allSearches.GroupBy(s => s.SearchedAt.Date)
                    .OrderByDescending(g => g.Key)
                    .Take(7)
                    .Select(g => new
                    {
                        date = g.Key.ToString("yyyy-MM-dd"),
                        count = g.Count()
                    })
            };

            return Ok(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving statistics");
            return StatusCode(500, new { error = "An error occurred while retrieving statistics" });
        }
    }

    /// <summary>
    /// Health check for analytics service
    /// </summary>
    [HttpGet("health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult HealthCheck()
    {
        return Ok(new
        {
            status = "healthy",
            service = "AnalyticsController",
            database = "In-Memory (Mock SQL Server)",
            timestamp = DateTime.UtcNow
        });
    }
}
