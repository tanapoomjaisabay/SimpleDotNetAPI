using BookingAPI.Domain.Entities;
using BookingAPI.Domain.Interfaces;
using BookingAPI.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingAPI.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for search history operations
/// Uses Entity Framework Core with In-Memory database (mock)
/// </summary>
public class SearchHistoryRepository : ISearchHistoryRepository
{
    private readonly BookingDbContext _context;
    private readonly ILogger<SearchHistoryRepository> _logger;

    public SearchHistoryRepository(
        BookingDbContext context,
        ILogger<SearchHistoryRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SearchHistory> SaveSearchHistoryAsync(SearchHistory searchHistory)
    {
        try
        {
            _logger.LogInformation(
                "Saving search history - Route: {Origin} to {Destination}, Fares Found: {Count}",
                searchHistory.Origin,
                searchHistory.Destination,
                searchHistory.TotalFaresFound);

            // Add to DbContext
            await _context.SearchHistories.AddAsync(searchHistory);

            // Save changes to database (in-memory)
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Search history saved successfully with ID: {Id}",
                searchHistory.Id);

            return searchHistory;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving search history");
            throw;
        }
    }

    public async Task<IEnumerable<SearchHistory>> GetRecentSearchesAsync(int limit = 10)
    {
        try
        {
            _logger.LogInformation("Retrieving {Limit} recent searches", limit);

            var recentSearches = await _context.SearchHistories
                .OrderByDescending(s => s.SearchedAt)
                .Take(limit)
                .ToListAsync();

            _logger.LogInformation("Retrieved {Count} recent searches", recentSearches.Count);

            return recentSearches;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving recent searches");
            throw;
        }
    }

    public async Task<SearchHistory?> GetByIdAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Retrieving search history by ID: {Id}", id);

            var searchHistory = await _context.SearchHistories
                .FirstOrDefaultAsync(s => s.Id == id);

            if (searchHistory == null)
            {
                _logger.LogWarning("Search history not found for ID: {Id}", id);
            }

            return searchHistory;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving search history by ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<(string Origin, string Destination, int SearchCount)>> GetPopularRoutesAsync(int topCount = 10)
    {
        try
        {
            _logger.LogInformation("Retrieving top {Count} popular routes", topCount);

            var popularRoutes = await _context.SearchHistories
                .GroupBy(s => new { s.Origin, s.Destination })
                .Select(g => new
                {
                    g.Key.Origin,
                    g.Key.Destination,
                    SearchCount = g.Count()
                })
                .OrderByDescending(x => x.SearchCount)
                .Take(topCount)
                .ToListAsync();

            var result = popularRoutes
                .Select(r => (r.Origin, r.Destination, r.SearchCount))
                .ToList();

            _logger.LogInformation("Retrieved {Count} popular routes", result.Count);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving popular routes");
            throw;
        }
    }
}
