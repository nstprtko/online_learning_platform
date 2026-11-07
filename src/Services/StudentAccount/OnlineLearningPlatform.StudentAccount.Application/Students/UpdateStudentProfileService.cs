using OnlineLearningPlatform.StudentAccount.Application.Interfaces;
using OnlineLearningPlatform.StudentAccount.Domain.Exceptions;

namespace OnlineLearningPlatform.StudentAccount.Application.Students;

public sealed class UpdateStudentProfileService
{
    private readonly IStudentRepository _repository;

    public UpdateStudentProfileService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        Guid studentId,
        string name,
        string email,
        CancellationToken cancellationToken = default)
    {
        var student = await _repository.GetByIdAsync(studentId, cancellationToken);

        if (student is null)
            throw new StudentNotFoundException(studentId);

        student.UpdateProfile(name, email);
        await _repository.UpdateAsync(student, cancellationToken);
    }
}