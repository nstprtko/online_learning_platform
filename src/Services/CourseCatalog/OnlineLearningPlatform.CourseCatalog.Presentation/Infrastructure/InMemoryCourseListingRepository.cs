using System.Collections.Concurrent;
using OnlineLearningPlatform.CourseCatalog.Application.Interfaces;
using OnlineLearningPlatform.CourseCatalog.Domain.Catalog;

namespace OnlineLearningPlatform.CourseCatalog.Presentation.Infrastructure;

public sealed class InMemoryCourseListingRepository : ICourseListingRepository
{
    private readonly ConcurrentDictionary<Guid, CourseListing> _listings = new();

    public Task AddAsync(
        CourseListing listing,
        CancellationToken cancellationToken = default)
    {
        _listings[listing.Id] = listing;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<CourseListing>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<CourseListing> result = _listings.Values.ToList();
        return Task.FromResult(result);
    }

    public Task<CourseListing?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        _listings.TryGetValue(id, out var listing);
        return Task.FromResult(listing);
    }

    public Task UpdateAsync(
        CourseListing listing,
        CancellationToken cancellationToken = default)
    {
        _listings[listing.Id] = listing;
        return Task.CompletedTask;
    }
}