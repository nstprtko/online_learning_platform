namespace OnlineLearningPlatform.CourseManagement.Domain.Exceptions;

public sealed class CourseCannotBePublishedException : DomainException
{
    public CourseCannotBePublishedException(string reason)
        : base($"Course cannot be published: {reason}")
    {
    }
}