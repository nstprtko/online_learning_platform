using OnlineLearningPlatform.CourseManagement.Domain.Exceptions;

namespace OnlineLearningPlatform.CourseManagement.Domain.Courses;

public sealed class Course
{
    private readonly List<CourseContent> _content = new();

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public bool IsPublished { get; private set; }

    public IReadOnlyCollection<CourseContent> Content => _content.AsReadOnly();

    public Course(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Course title cannot be empty.");

        Id = Guid.NewGuid();
        Title = title;
    }

    public void SetDescription(string description)
    {
        Description = description;
    }

    public void AddContent(CourseContent content)
    {
        if (content is null)
            throw new CourseContentException("Content cannot be null.");

        _content.Add(content);
    }

    public void Publish()
    {
        if (IsPublished)
            throw new CourseCannotBePublishedException(
                "The course has already been published.");

        if (string.IsNullOrWhiteSpace(Description))
            throw new CourseCannotBePublishedException(
                "A course must have a description.");

        if (_content.Count == 0)
            throw new CourseCannotBePublishedException(
                "A course must contain at least one content item.");

        IsPublished = true;
    }
}
