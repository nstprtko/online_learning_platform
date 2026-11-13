
using OnlineLearningPlatform.CourseManagement.Application.Courses;
using OnlineLearningPlatform.CourseManagement.Infrastructure;
using OnlineLearningPlatform.CourseManagement.Infrastructure.Persistence.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<CreateCourseService>();
builder.Services.AddScoped<PublishCourseService>();
builder.Services.AddScoped<SetCourseDescriptionService>();
builder.Services.AddScoped<AddCourseContentService>();

builder.Services.AddScoped<GetAllCoursesService>();
builder.Services.AddScoped<GetCourseByIdService>();
builder.Services.AddScoped<DeleteCourseService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();

    var seeder = scope.ServiceProvider
        .GetRequiredService<CourseManagementSeeder>();

    await seeder.SeedAsync();

    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
