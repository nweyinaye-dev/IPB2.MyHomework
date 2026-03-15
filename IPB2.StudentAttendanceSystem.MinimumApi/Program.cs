
using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.MinimumApi.Common;
using IPB2.StudentAttendanceSystem.MinimumApi.Features.Attendance;
using IPB2.StudentAttendanceSystem.MinimumApi.Features.Class;
using IPB2.StudentAttendanceSystem.MinimumApi.Features.Grade;
using IPB2.StudentAttendanceSystem.MinimumApi.Features.Leave;
using IPB2.StudentAttendanceSystem.MinimumApi.Features.Report;
using IPB2.StudentAttendanceSystem.MinimumApi.Features.Schedule;
using IPB2.StudentAttendanceSystem.MinimumApi.Features.StudentEnroll;
using IPB2.StudentAttendanceSystem.MinimumApi.Features.Teacher;
using IPB2.StudentLeaveSystem.MinimumApi.Features.Leave;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>{});
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

# region teacher controller
var teacherApi = app.MapGroup("/api/teachers").WithTags("Teacher Endpoints");

// GET: /api/teachers/{pageNo}/{pageSize}
teacherApi.MapGet("/{pageNo:int}/{pageSize:int}", async (int pageNo, int pageSize, ITeacherService _teacherService) =>
{
    if (pageNo < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
    if (pageSize < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

    var result = await _teacherService.GetAllTeacherAsync(pageNo, pageSize);
    string message = result.Count > 0 ? "Get all Teacher successfully." : "No data.";

    return Results.Ok(new GetAllTeacherResponse
    {
        IsSuccess = true,
        Message = message,
        data = result
    });
});

// POST: /api/teachers
teacherApi.MapPost("/", async (CreateTeacherRequest request, ITeacherService _teacherService) =>
{
    var validationRes = ValidateTeacher(request);
    if (!validationRes.IsSuccess) return Results.BadRequest(validationRes);

    var response = await _teacherService.SaveTeacherAsync(request);
    return ResponseHelper.ConvertResponseType(response);
});

// PUT: /api/teachers/{id}
teacherApi.MapPut("/{id}", async (string id, CreateTeacherRequest request, ITeacherService _teacherService) =>
{
    var validationRes = ValidateTeacher(request);
    if (!validationRes.IsSuccess) return Results.BadRequest(validationRes);

    var response = await _teacherService.UpdateTeacherAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// PATCH: /api/teachers/{id}
teacherApi.MapPatch("/{id}", async (string id, UpdatePatchTeacherRequest request, ITeacherService _teacherService) =>
{
    var response = await _teacherService.UpdatePatchTeacherAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// DELETE: /api/teachers/{id}
teacherApi.MapDelete("/{id}", async (string id, ITeacherService _teacherService) =>
{
    var response = await _teacherService.DeleteTeacherAsync(id);
    return ResponseHelper.ConvertResponseType(response);
});

// 3. Validation Helper
static ResponseBaseModel ValidateTeacher(CreateTeacherRequest request)
{
    if (string.IsNullOrWhiteSpace(request.TeacherName))
        return new ResponseBaseModel { IsSuccess = false, Message = "Teacher name is required." };
    if (string.IsNullOrWhiteSpace(request.TeacherPhoneno))
        return new ResponseBaseModel { IsSuccess = false, Message = "Teacher phone no is required." };
    if (string.IsNullOrWhiteSpace(request.TeacherAddress))
        return new ResponseBaseModel { IsSuccess = false, Message = "Teacher address is required." };

    return new ResponseBaseModel { IsSuccess = true };
}
# endregion

#region schedule controller
// Schedules Group
var schedules = app.MapGroup("/api/schedules").WithTags("Schedule Endpoints");

schedules.MapGet("/{pageNo}/{pageSize}", async (int pageNo, int pageSize, IScheduleService _scheduleService) =>
{
    if (pageNo < 0) return Results.BadRequest(new { IsSuccess = false, Message = "Invalid page number." });
    if (pageSize < 0) return Results.BadRequest(new { IsSuccess = false, Message = "Invalid page size." });

    var result = await _scheduleService.GetAllScheduleAsync(pageNo, pageSize);
    string message = result.Count > 0 ? "Get all schedule successfully." : "No data.";

    return Results.Ok(new { IsSuccess = true, Message = message, data = result });
});

schedules.MapPost("/", async (CreateScheduleRequest request, IScheduleService _scheduleService) =>
{
    var validationRes = ValidateSchedule(request);
    if (!validationRes.IsSuccess) return Results.BadRequest(validationRes);

    var response = await _scheduleService.SaveScheduleAsync(request);
    return ResponseHelper.ConvertResponseType(response);
});

schedules.MapPut("/{id}", async (string id, CreateScheduleRequest request, IScheduleService _scheduleService) =>
{
    var validationRes = ValidateSchedule(request);
    if (!validationRes.IsSuccess) return Results.BadRequest(validationRes);

    var response = await _scheduleService.UpdateScheduleAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

schedules.MapPatch("/{id}", async (string id, UpdatePatchScheduleRequest request, IScheduleService _scheduleService) =>
{
    var response = await _scheduleService.UpdatePatchScheduleAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

schedules.MapDelete("/{id}", async (string id, IScheduleService _scheduleService) =>
{
    var response = await _scheduleService.DeleteScheduleAsync(id);
    return ResponseHelper.ConvertResponseType(response);
});
static ResponseBaseModel ValidateSchedule(CreateScheduleRequest request)
{
    if (string.IsNullOrWhiteSpace(request.ScheduleName))
        return new ResponseBaseModel { IsSuccess = false, Message = "Schedule name is required." };

    if (!TimeSpan.TryParseExact(request.StartTime, "hh\\:mm\\:ss", null, out var startTime))
        return new ResponseBaseModel { IsSuccess = false, Message = "StartTime must be in HH:mm:ss format" };

    if (!TimeSpan.TryParseExact(request.EndTime, "hh\\:mm\\:ss", null, out var endTime))
        return new ResponseBaseModel { IsSuccess = false, Message = "EndTime must be in HH:mm:ss format" };

    if (endTime <= startTime)
        return new ResponseBaseModel { IsSuccess = false, Message = "EndTime must be after StartTime" };

    return new ResponseBaseModel { IsSuccess = true };
}
#endregion

# region class controller
var classApi = app.MapGroup("/api/class").WithTags("Class Endpoints"); ;

// GET: /api/class/{pageNo}/{pageSize}
classApi.MapGet("/{pageNo:int}/{pageSize:int}", async (int pageNo, int pageSize, IClassService _classService) =>
{
    if (pageNo < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
    if (pageSize < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

    var result = await _classService.GetAllClassAsync(pageNo, pageSize);
    string message = result.Count > 0 ? "Get all Class successfully." : "No data.";

    return Results.Ok(new GetAllClassResponse
    {
        IsSuccess = true,
        Message = message,
        data = result
    });
});

// POST: /api/class
classApi.MapPost("/", async (CreateClassRequest request, IClassService _classService) =>
{
    var validationRes = ValidateClass(request);
    if (!validationRes.IsSuccess) return Results.BadRequest(validationRes);

    var response = await _classService.SaveClassAsync(request);
    return ResponseHelper.ConvertResponseType(response);
});

// PUT: /api/class/{id}
classApi.MapPut("/{id}", async (string id, CreateClassRequest request, IClassService _classService) =>
{
    var response = await _classService.UpdateClassAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// PATCH: /api/class/{id}
classApi.MapPatch("/{id}", async (string id, UpdatePatchClassRequest request, IClassService _classService) =>
{
    var response = await _classService.UpdatePatchClassAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// DELETE: /api/class/{id}
classApi.MapDelete("/{id}", async (string id, IClassService _classService) =>
{
    var response = await _classService.DeleteClassAsync(id);
    return ResponseHelper.ConvertResponseType(response);
});

// 3. Validation Logic
static ResponseBaseModel ValidateClass(CreateClassRequest request)
{
    if (string.IsNullOrWhiteSpace(request.ClassName))
        return new ResponseBaseModel { IsSuccess = false, Message = "Class name is required." };

    if (request.Duration == 0)
        return new ResponseBaseModel { IsSuccess = false, Message = "Class duration is required." };

    if (string.IsNullOrWhiteSpace(request.ScheduleId))
        return new ResponseBaseModel { IsSuccess = false, Message = "Class schedule id is required." };

    if (string.IsNullOrWhiteSpace(request.TeacherId))
        return new ResponseBaseModel { IsSuccess = false, Message = "Class teacher id is required." };

    return new ResponseBaseModel { IsSuccess = true };
}
# endregion

# region grade controller
var gradesApi = app.MapGroup("/api/grades").WithTags("Grade Endpoints"); ;

// GET: /api/grades/{pageNo}/{pageSize}
gradesApi.MapGet("/{pageNo:int}/{pageSize:int}", async (int pageNo, int pageSize, IGradeService _gradeService) =>
{
    if (pageNo < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
    if (pageSize < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

    var result = await _gradeService.GetAllGradeAsync(pageNo, pageSize);
    string message = result.Count > 0 ? "Get all Grade successfully." : "No data.";

    return Results.Ok(new GetAllGradeResponse
    {
        IsSuccess = true,
        Message = message,
        data = result
    });
});

// POST: /api/grades
gradesApi.MapPost("/", async (CreateGradeRequest request, IGradeService _gradeService) =>
{
    var validationRes = ValidateGrade(request);
    if (!validationRes.IsSuccess) return Results.BadRequest(validationRes);

    var response = await _gradeService.SaveGradeAsync(request);
    return ResponseHelper.ConvertResponseType(response);
});

// PUT: /api/grades/{id}
gradesApi.MapPut("/{id}", async (string id, CreateGradeRequest request, IGradeService _gradeService) =>
{
    var response = await _gradeService.UpdateGradeAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// PATCH: /api/grades/{id}
gradesApi.MapPatch("/{id}", async (string id, UpdatePatchGradeRequest request, IGradeService _gradeService) =>
{
    var response = await _gradeService.UpdatePatchGradeAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// DELETE: /api/grades/{id}
gradesApi.MapDelete("/{id}", async (string id, IGradeService _gradeService) =>
{
    var response = await _gradeService.DeleteGradeAsync(id);
    return ResponseHelper.ConvertResponseType(response);
});

// 3. Validation Logic
static ResponseBaseModel ValidateGrade(CreateGradeRequest request)
{
    if (string.IsNullOrWhiteSpace(request.GradeName))
        return new ResponseBaseModel { IsSuccess = false, Message = "Grade name is required." };
    if (request.FromPercent == 0)
        return new ResponseBaseModel { IsSuccess = false, Message = "Grade fromPercent is required." };
    if (request.ToPercent == 0)
        return new ResponseBaseModel { IsSuccess = false, Message = "Grade toPercent is required." };

    return new ResponseBaseModel { IsSuccess = true };
}
# endregion

# region studentEnroll controller
var enrollApi = app.MapGroup("/api/students-enroll").WithTags("Student Enroll Endpoints"); ;

// GET: /api/students-enroll/{pageNo}/{pageSize}
enrollApi.MapGet("/{pageNo:int}/{pageSize:int}", async (int pageNo, int pageSize, IStudentsEnrollService _service) =>
{
    if (pageNo < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
    if (pageSize < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

    var result = await _service.GetAllStudentsEnrollAsync(pageNo, pageSize);
    string message = result.Count > 0 ? "Get all StudentsEnroll successfully." : "No data.";

    return Results.Ok(new GetAllStudentsEnrollResponse
    {
        IsSuccess = true,
        Message = message,
        data = result
    });
});

// POST: /api/students-enroll
enrollApi.MapPost("/", async (CreateStudentsEnrollRequest request, IStudentsEnrollService _service) =>
{
    var validationRes = ValidateEnrollment(request);
    if (!validationRes.IsSuccess) return Results.BadRequest(validationRes);

    var response = await _service.SaveStudentsEnrollAsync(request);
    return ResponseHelper.ConvertResponseType(response);
});

// PUT: /api/students-enroll/{id}
enrollApi.MapPut("/{id}", async (string id, CreateStudentsEnrollRequest request, IStudentsEnrollService _service) =>
{
    var response = await _service.UpdateStudentsEnrollAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// PATCH: /api/students-enroll/{id}
enrollApi.MapPatch("/{id}", async (UpdatePatchStudentsEnrollRequest request, string id, IStudentsEnrollService _service) =>
{
    var response = await _service.UpdatePatchStudentsEnrollAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// DELETE: /api/students-enroll/{id}
enrollApi.MapDelete("/{id}", async (string id, IStudentsEnrollService _service) =>
{
    var response = await _service.DeleteStudentsEnrollAsync(id);
    return ResponseHelper.ConvertResponseType(response);
});

// 3. Validation Logic
static ResponseBaseModel ValidateEnrollment(CreateStudentsEnrollRequest request)
{
    if (string.IsNullOrWhiteSpace(request.StudentName))
        return new ResponseBaseModel { IsSuccess = false, Message = "Student name is required." };
    if (string.IsNullOrWhiteSpace(request.StudentPhoneno))
        return new ResponseBaseModel { IsSuccess = false, Message = "Student phoneno is required." };
    if (request.StudentAge == 0)
        return new ResponseBaseModel { IsSuccess = false, Message = "Student age is required." };

    return new ResponseBaseModel { IsSuccess = true };
}
# endregion

# region attendance controller
var attendanceApi = app.MapGroup("/api/attendances").WithTags("Attendance Endpoints"); ;

// GET: /api/attendances/{pageNo}/{pageSize}
attendanceApi.MapGet("/{pageNo:int}/{pageSize:int}", async (int pageNo, int pageSize, IAttendanceService _service) =>
{
    if (pageNo < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
    if (pageSize < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

    var result = await _service.GetAllAttendanceAsync(pageNo, pageSize);
    string message = result.Count > 0 ? "Get all Attendance successfully." : "No data.";

    return Results.Ok(new GetAllAttendanceResponse
    {
        IsSuccess = true,
        Message = message,
        data = result
    });
});

// POST: /api/attendances
attendanceApi.MapPost("/", async (CreateAttendanceRequest request, IAttendanceService _service) =>
{
    var validationRes = ValidateAttendance(request);
    if (!validationRes.IsSuccess) return Results.BadRequest(validationRes);

    var response = await _service.SaveAttendanceAsync(request);
    return ResponseHelper.ConvertResponseType(response);
});

// PUT: /api/attendances/{id}
attendanceApi.MapPut("/{id}", async (string id, CreateAttendanceRequest request, IAttendanceService _service) =>
{
    var validationRes = ValidateAttendance(request);
    if (!validationRes.IsSuccess) return Results.BadRequest(validationRes);

    var response = await _service.UpdateAttendanceAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// PATCH: /api/attendances/{id}
attendanceApi.MapPatch("/{id}", async (string id, UpdatePatchAttendanceRequest request, IAttendanceService _service) =>
{
    var response = await _service.UpdatePatchAttendanceAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// DELETE: /api/attendances/{id}
attendanceApi.MapDelete("/{id}", async (string id, IAttendanceService _service) =>
{
    var response = await _service.DeleteAttendanceAsync(id);
    return ResponseHelper.ConvertResponseType(response);
});

// POST: /api/attendances/calculate
attendanceApi.MapPost("/calculate", async (CalculateAttendanceRequest request, IAttendanceService _service) =>
{
    if (string.IsNullOrWhiteSpace(request.StudentEnrollId))
        return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Student id is required." });

    var response = await _service.CalculateAttendanceAsync(request);
    return ResponseHelper.ConvertResponseType(response);
});

// 3. Validation Logic
static ResponseBaseModel ValidateAttendance(CreateAttendanceRequest request)
{
    if (string.IsNullOrWhiteSpace(request.ClassId))
        return new ResponseBaseModel { IsSuccess = false, Message = "Class id is required." };
    if (string.IsNullOrWhiteSpace(request.StudentEnrollId))
        return new ResponseBaseModel { IsSuccess = false, Message = "Student id is required." };

    if (!TimeSpan.TryParseExact(request.TimeIn, "hh\\:mm\\:ss", CultureInfo.InvariantCulture, out var startTime))
        return new ResponseBaseModel { IsSuccess = false, Message = "Timein must be in HH:mm:ss format" };

    if (!TimeSpan.TryParseExact(request.TimeOut, "hh\\:mm\\:ss", CultureInfo.InvariantCulture, out var endTime))
        return new ResponseBaseModel { IsSuccess = false, Message = "Timeout must be in HH:mm:ss format" };

    if (endTime <= startTime)
        return new ResponseBaseModel { IsSuccess = false, Message = "Timeout must be after Timein" };

    return new ResponseBaseModel { IsSuccess = true };
}
# endregion

# region leave controller
var leaveApi = app.MapGroup("/api/leaves").WithTags("Leave Endpoints"); ;

// GET: /api/leaves/{pageNo}/{pageSize}
leaveApi.MapGet("/{pageNo:int}/{pageSize:int}", async (int pageNo, int pageSize, ILeaveService _service) =>
{
    if (pageNo < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
    if (pageSize < 0) return Results.BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

    var result = await _service.GetAllLeaveAsync(pageNo, pageSize);
    string message = result.Count > 0 ? "Get all Leave successfully." : "No data.";

    return Results.Ok(new GetAllLeaveResponse
    {
        IsSuccess = true,
        Message = message,
        data = result
    });
});

// POST: /api/leaves
leaveApi.MapPost("/", async (CreateLeaveRequest request, ILeaveService _service) =>
{
    var validationRes = ValidateLeave(request);
    if (!validationRes.IsSuccess) return Results.BadRequest(validationRes);

    var response = await _service.SaveLeaveAsync(request);
    return ResponseHelper.ConvertResponseType(response);
});

// PUT: /api/leaves/{id}
leaveApi.MapPut("/{id}", async (string id, CreateLeaveRequest request, ILeaveService _service) =>
{
    var response = await _service.UpdateLeaveAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// PATCH: /api/leaves/{id}
leaveApi.MapPatch("/{id}", async (string id, UpdatePatchLeaveRequest request, ILeaveService _service) =>
{
    var response = await _service.UpdatePatchLeaveAsync(request, id);
    return ResponseHelper.ConvertResponseType(response);
});

// DELETE: /api/leaves/{id}
leaveApi.MapDelete("/{id}", async (string id, ILeaveService _service) =>
{
    var response = await _service.DeleteLeaveAsync(id);
    return ResponseHelper.ConvertResponseType(response);
});

// 3. Static Validation Helper
static ResponseBaseModel ValidateLeave(CreateLeaveRequest request)
{
    if (string.IsNullOrWhiteSpace(request.ClassId))
        return new ResponseBaseModel { IsSuccess = false, Message = "Class id is required." };
    if (string.IsNullOrWhiteSpace(request.StudentEnrollId))
        return new ResponseBaseModel { IsSuccess = false, Message = "Student id is required." };

    return new ResponseBaseModel { IsSuccess = true };
}
# endregion

# region report controller
var reportApi = app.MapGroup("/api/reports").WithTags("Report Endpoints"); ;

// POST: /api/reports/attendance
reportApi.MapPost("/attendance", async (AttendanceReportRequest request, IReportService _service) =>
{
    var validationRes = ValidateReportRequest(request);
    if (!validationRes.IsSuccess)
        return Results.BadRequest(validationRes);

    var response = await _service.GetAttendanceReportAsync(request);

    return response.IsSuccess
        ? Results.Ok(response)
        : Results.NotFound(response);
});

// POST: /api/reports/summary
reportApi.MapPost("/summary", async (AttendanceReportRequest request, IReportService _service) =>
{
    var validationRes = ValidateReportRequest(request);
    if (!validationRes.IsSuccess)
        return Results.BadRequest(validationRes);

    var response = await _service.GetAttendanceSummaryReportAsync(request);

    return response.IsSuccess
        ? Results.Ok(response)
        : Results.NotFound(response);
});

// 3. Validation Logic
static ResponseBaseModel ValidateReportRequest(AttendanceReportRequest request)
{
    if (string.IsNullOrWhiteSpace(request.ClassId))
        return new ResponseBaseModel { IsSuccess = false, Message = "Class id is required." };
    if (string.IsNullOrWhiteSpace(request.StudentEnrollId))
        return new ResponseBaseModel { IsSuccess = false, Message = "Student id is required." };

    return new ResponseBaseModel { IsSuccess = true };
}
# endregion 

app.Run();

