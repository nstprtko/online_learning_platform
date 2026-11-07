using OnlineLearningPlatform.CourseCatalog.Application.Interfaces;
using OnlineLearningPlatform.CourseCatalog.Domain.Catalog;

namespace OnlineLearningPlatform.CourseCatalog.Application.Catalog;

public sealed class ToggleCourseFavouriteService
{
    private readonly ICourseListingRepository _repository;

    public ToggleCourseFavouriteService(ICourseListingRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        CourseListing? listing = await _repository.GetByIdAsync(
            courseId, cancellationToken);

        if (listing is null)
            throw new KeyNotFoundException($"Course listing '{courseId}' was not found.");

        if (listing.IsFavourite)
            listing.RemoveFromFavourites();
        else
            listing.AddToFavourites();

        await _repository.UpdateAsync(listing, cancellationToken);
    }
}