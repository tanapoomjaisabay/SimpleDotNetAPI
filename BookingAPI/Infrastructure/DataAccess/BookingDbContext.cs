using BookingAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingAPI.Infrastructure.DataAccess;

/// <summary>
/// Entity Framework DbContext for BookingAPI
/// Currently using In-Memory database for mock/testing
/// Can be switched to SQL Server by changing configuration
/// </summary>
public class BookingDbContext : DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Search history table
    /// </summary>
    public DbSet<SearchHistory> SearchHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure SearchHistory entity
        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Origin)
                .IsRequired()
                .HasMaxLength(3);

            entity.Property(e => e.Destination)
                .IsRequired()
                .HasMaxLength(3);

            entity.Property(e => e.CabinClass)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.UserId)
                .HasMaxLength(100);

            entity.Property(e => e.CorrelationId)
                .HasMaxLength(100);

            entity.Property(e => e.DepartureDate)
                .IsRequired();

            entity.Property(e => e.SearchedAt)
                .IsRequired();

            entity.Property(e => e.PassengerCount)
                .IsRequired();

            entity.Property(e => e.TotalFaresFound)
                .IsRequired();

            // Indexes for better query performance
            entity.HasIndex(e => e.SearchedAt);

            entity.HasIndex(e => new { e.Origin, e.Destination });

            entity.HasIndex(e => e.CorrelationId);
        });
    }
}
