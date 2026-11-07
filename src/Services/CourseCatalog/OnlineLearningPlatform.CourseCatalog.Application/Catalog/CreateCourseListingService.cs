using OnlineLearningPlatform.CourseCatalog.Application.Interfaces;
using OnlineLearningPlatform.CourseCatalog.Domain.Catalog;

namespace OnlineLearningPlatform.CourseCatalog.Application.Catalog;

public sealed class CreateCourseListingService
{
    private readonly ICourseListingRepository _repository;

    public CreateCourseListingService(ICourseListingRepository repository)
    {
        _repository = repository;
    }

    public async Task<CourseListing> ExecuteAsync(
        Guid id,
        string title,
        string description,
        CancellationToken cancellationToken = default)
    {
        var listing = new CourseListing(id, title, description);

        await _repository.AddAsync(listing, cancellationToken);

        return listing;
    }
}