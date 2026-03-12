using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Features.Attendance;
using IPB2.StudentAttendanceSystem.WebApi.Features.Class;
using IPB2.StudentAttendanceSystem.WebApi.Features.Grade;
using IPB2.StudentAttendanceSystem.WebApi.Features.Leave;
using IPB2.StudentAttendanceSystem.WebApi.Features.Report;
using IPB2.StudentAttendanceSystem.WebApi.Features.Schedule;
using IPB2.StudentAttendanceSystem.WebApi.Features.StudentEnroll;
using IPB2.StudentAttendanceSystem.WebApi.Features.Teacher;
using IPB2.StudentLeaveSystem.WebApi.Features.Leave;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OrderActionsBy((apiDesc) =>
    {
        var controllerName = apiDesc.ActionDescriptor.RouteValues["controller"];

        // Define your custom order here
        var order = controllerName switch
        {
            "Schedules" => "1",
            "Teachers" => "2",
            "Class" => "3",
            "Grades" => "4",
            "StudentsEnroll" => "5",
            "Leaves" => "6",            
            "Attendances" => "7",
            "Reports" => "8",
            _ => "99" // Everything else goes to the bottom
        };

        return order;
    });
});

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
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IReportService, ReportService>();

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
