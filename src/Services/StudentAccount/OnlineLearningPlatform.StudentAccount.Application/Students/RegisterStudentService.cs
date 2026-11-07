using OnlineLearningPlatform.StudentAccount.Application.Interfaces;
using OnlineLearningPlatform.StudentAccount.Domain.Students;

namespace OnlineLearningPlatform.StudentAccount.Application.Students;

public sealed class RegisterStudentService
{
    private readonly IStudentRepository _repository;

    public RegisterStudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Student> ExecuteAsync(
        string name,
        string email,
        CancellationToken cancellationToken = default)
    {
        var student = new Student(name, email);
        await _repository.AddAsync(student, cancellationToken);
        return student;
    }
}