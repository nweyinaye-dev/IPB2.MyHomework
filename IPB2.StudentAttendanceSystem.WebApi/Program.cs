using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Schedule;

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
