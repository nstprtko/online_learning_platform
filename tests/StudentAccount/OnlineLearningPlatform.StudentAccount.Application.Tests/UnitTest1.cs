using Moq;
using OnlineLearningPlatform.StudentAccount.Application.Interfaces;
using OnlineLearningPlatform.StudentAccount.Application.Students;
using OnlineLearningPlatform.StudentAccount.Domain.Students;

namespace OnlineLearningPlatform.StudentAccount.Application.Tests;

public class StudentServiceTests
{
    [Fact]
    public async Task RegisterStudent_SavesStudentToRepository()
    {
        var repository = new Mock<IStudentRepository>();
        var service = new RegisterStudentService(repository.Object);

        var result = await service.ExecuteAsync("Alex", "alex@example.com");

        Assert.Equal("Alex", result.Name);
        repository.Verify(
            r => r.AddAsync(
                It.Is<Student>(s => s.Email == "alex@example.com"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
