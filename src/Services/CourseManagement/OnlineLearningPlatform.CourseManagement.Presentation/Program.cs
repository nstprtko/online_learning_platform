using OnlineLearningPlatform.CourseManagement.Application.Courses;
using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Presentation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ICourseRepository, InMemoryCourseRepository>();

builder.Services.AddScoped<CreateCourseService>();
builder.Services.AddScoped<PublishCourseService>();

builder.Services.AddScoped<SetCourseDescriptionService>();
builder.Services.AddScoped<AddCourseContentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();