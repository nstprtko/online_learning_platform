using OnlineLearningPlatform.Enrollment.Application.Enrollments;
using OnlineLearningPlatform.Enrollment.Application.Interfaces;
using OnlineLearningPlatform.Enrollment.Presentation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IEnrollmentRepository, InMemoryEnrollmentRepository>();

builder.Services.AddScoped<EnrollStudentService>();
builder.Services.AddScoped<CancelEnrollmentService>();
builder.Services.AddScoped<CheckCourseAccessService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();