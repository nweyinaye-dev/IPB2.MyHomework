using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Features.Attendance;
using IPB2.StudentAttendanceSystem.WebApi.Features.Class;
using IPB2.StudentAttendanceSystem.WebApi.Features.Grade;
using IPB2.StudentAttendanceSystem.WebApi.Features.Schedule;
using IPB2.StudentAttendanceSystem.WebApi.Features.StudentEnroll;
using IPB2.StudentAttendanceSystem.WebApi.Features.Teacher;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // If your AppDbContext already has OnConfiguring, this can be left empty.
    // But recommended is to configure connection string here.
    // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IStudentsEnrollService, StudentsEnrollService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
