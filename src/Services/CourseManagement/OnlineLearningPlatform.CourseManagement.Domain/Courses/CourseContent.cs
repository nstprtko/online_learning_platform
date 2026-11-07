namespace OnlineLearningPlatform.CourseManagement.Domain.Courses;

public sealed class CourseContent
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public CourseContent(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Content title cannot be empty.");

        Id = Guid.NewGuid();
        Title = title;
    }
}