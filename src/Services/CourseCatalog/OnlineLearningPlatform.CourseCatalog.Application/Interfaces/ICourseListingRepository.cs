using OnlineLearningPlatform.CourseCatalog.Domain.Catalog;

namespace OnlineLearningPlatform.CourseCatalog.Application.Interfaces;

public interface ICourseListingRepository
{
    Task AddAsync(CourseListing listing, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<CourseListing>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<CourseListing?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(CourseListing listing, CancellationToken cancellationToken = default);
}