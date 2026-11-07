using OnlineLearningPlatform.CourseCatalog.Application.Interfaces;
using OnlineLearningPlatform.CourseCatalog.Domain.Catalog;

namespace OnlineLearningPlatform.CourseCatalog.Application.Catalog;

public sealed class SearchCoursesService
{
    private readonly ICourseListingRepository _repository;

    public SearchCoursesService(ICourseListingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<CourseListing>> ExecuteAsync(
        string searchTerm,
        CancellationToken cancellationToken = default)
    {
        var listings = await _repository.GetAllAsync(cancellationToken);

        return listings
            .Where(course =>
                course.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                course.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}