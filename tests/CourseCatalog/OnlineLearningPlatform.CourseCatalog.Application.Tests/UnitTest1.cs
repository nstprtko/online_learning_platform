using Moq;
using OnlineLearningPlatform.CourseCatalog.Application.Catalog;
using OnlineLearningPlatform.CourseCatalog.Application.Interfaces;
using OnlineLearningPlatform.CourseCatalog.Domain.Catalog;

namespace OnlineLearningPlatform.CourseCatalog.Application.Tests;

public class CourseCatalogServiceTests
{
    [Fact]
    public async Task CreateCourseListing_SavesListingToRepository()
    {
        var repository = new Mock<ICourseListingRepository>();
        var service = new CreateCourseListingService(repository.Object);
        var id = Guid.NewGuid();

        var result = await service.ExecuteAsync(id, "C# Basics", "Learn C#");

        Assert.Equal(id, result.Id);
        repository.Verify(
            r => r.AddAsync(
                It.Is<CourseListing>(c => c.Id == id && c.Title == "C# Basics"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
