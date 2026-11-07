using OnlineLearningPlatform.StudentAccount.Domain.Students;

namespace OnlineLearningPlatform.StudentAccount.Domain.Tests;

public class StudentTests
{
    [Fact]
    public void Constructor_CreatesStudent()
    {
        var student = new Student("Alex", "alex@example.com");

        Assert.Equal("Alex", student.Name);
        Assert.Equal("alex@example.com", student.Email);
        Assert.NotEqual(Guid.Empty, student.Id);
    }

    [Fact]
    public void UpdateProfile_ChangesNameAndEmail()
    {
        var student = new Student("Alex", "alex@example.com");

        student.UpdateProfile("Sam", "sam@example.com");

        Assert.Equal("Sam", student.Name);
        Assert.Equal("sam@example.com", student.Email);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid-email")]
    public void Constructor_RejectsInvalidEmail(string email)
    {
        Assert.Throws<ArgumentException>(() => new Student("Alex", email));
    }
}
