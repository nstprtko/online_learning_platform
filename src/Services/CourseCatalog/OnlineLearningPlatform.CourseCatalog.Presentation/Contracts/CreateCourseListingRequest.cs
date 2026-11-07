namespace OnlineLearningPlatform.CourseCatalog.Presentation.Contracts;

public sealed record CreateCourseListingRequest(
    Guid CourseId,
    string Title,
    string Description);