using BookingAPI.Domain.Entities;

namespace BookingAPI.Domain.Interfaces;

/// <summary>
/// Repository interface for search history operations
/// Follows Repository pattern for data access abstraction
/// </summary>
public interface ISearchHistoryRepository
{
    /// <summary>
    /// Saves a search history record
    /// </summary>
    /// <param name="searchHistory">The search history entity to save</param>
    /// <returns>The saved search history with generated ID</returns>
    Task<SearchHistory> SaveSearchHistoryAsync(SearchHistory searchHistory);

    /// <summary>
    /// Gets recent search history (optional - for future analytics)
    /// </summary>
    /// <param name="limit">Maximum number of records to return</param>
    /// <returns>List of recent searches</returns>
    Task<IEnumerable<SearchHistory>> GetRecentSearchesAsync(int limit = 10);

    /// <summary>
    /// Gets search history by ID (optional - for future features)
    /// </summary>
    /// <param name="id">Search history ID</param>
    /// <returns>Search history or null if not found</returns>
    Task<SearchHistory?> GetByIdAsync(Guid id);

    /// <summary>
    /// Gets popular routes based on search frequency (optional - for analytics)
    /// </summary>
    /// <param name="topCount">Number of top routes to return</param>
    /// <returns>Popular route statistics</returns>
    Task<IEnumerable<(string Origin, string Destination, int SearchCount)>> GetPopularRoutesAsync(int topCount = 10);
}
