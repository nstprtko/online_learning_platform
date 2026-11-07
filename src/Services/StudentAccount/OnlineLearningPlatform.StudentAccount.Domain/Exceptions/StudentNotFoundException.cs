namespace OnlineLearningPlatform.StudentAccount.Domain.Exceptions;

public sealed class StudentNotFoundException : Exception
{
    public StudentNotFoundException(Guid studentId)
        : base($"Student with ID '{studentId}' was not found.")
    {
    }
}