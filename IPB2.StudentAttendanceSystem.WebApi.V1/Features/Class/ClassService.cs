using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Class
{
    public class ClassService
    {
        private readonly AppDbContext _dbContext = new AppDbContext();
       
       
        private IQueryable<TblClass> ClassQuery()
        {
            IQueryable<TblClass> query = _dbContext.TblClasses
                   //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        private IQueryable<TblSchedule> ScheduleQuery()
        {
            IQueryable<TblSchedule> query = _dbContext.TblSchedules
                   //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        private IQueryable<TblTeacher> TeacherQuery()
        {
            IQueryable<TblTeacher> query = _dbContext.TblTeachers
                   //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "IPB2",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true,
        };

        public async Task<List<ClassModel>> GetAllClassAsync(int pageNo, int pageSize)
        {
            using (IDbConnection db = new SqlConnection(connectionString.ConnectionString))
            {
                db.Open();
                int offset = (pageNo - 1) * pageSize;
                var sql = $@"SELECT 
                                cls.[Id],
                                cls.[ClassName],
                                cls.[StartDate],
                                cls.[EndDate],
                                cls.[Duration],
                                sch.[ScheduleName],
                                tch.[TeacherName]
                            FROM [dbo].[Tbl_Class] cls
                            INNER JOIN [dbo].[Tbl_Teacher] tch ON cls.[TeacherID] = tch.[Id]
                            INNER JOIN [dbo].[Tbl_Schedule] sch ON cls.[ScheduleID] = sch.[Id]
                            WHERE cls.[isDelete] = 0
                            ORDER BY cls.[ClassName] ASC
                            OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";

                var result = await db.QueryAsync<ClassModel>(sql, new { Offset = offset, PageSize = pageSize });
                return result.ToList();
            }
        }
        public async Task<ServiceResponse> SaveClassAsync(CreateClassRequest req)
        {
            var isScheduleExist = await ScheduleQuery().AnyAsync(x => x.Id == req.ScheduleId);

            if (!isScheduleExist)            
                return new ServiceResponse { Status = ResponseTypes.NotFound,Message = "Schedule id not found."};            

            var isTeacherExist = await TeacherQuery().AnyAsync(x => x.Id == req.TeacherId);

            if (!isTeacherExist)            
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Teacher id not found."};
            

            await _dbContext.TblClasses.AddAsync(new TblClass
            {
                Id = Guid.NewGuid().ToString(),
                ClassName = req.ClassName,
                StartDate = req.StartDate,
                EndDate = req.EndDate,
                Duration = req.Duration,
                ScheduleId = req.ScheduleId,
                TeacherId = req.TeacherId,
                IsDelete = false
            });

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Class created successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
        public async Task<ServiceResponse> DeleteClassAsync(string id)
        {
            var item = await ClassQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (item is null) return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class not found." };
            // else if (item.IsDelete) return ResponseTypes.AlreadyDeleted;
            item.IsDelete = true;
            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Class deleted successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
        public async Task<ServiceResponse> UpdateClassAsync(CreateClassRequest request, string id)
        {
            var item = await ClassQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null) 
                return new ServiceResponse { Status = ResponseTypes.NotFound,Message = "Class not found."};

            var isScheduleExist = await ScheduleQuery().AnyAsync(x => x.Id == request.ScheduleId);

            if (!isScheduleExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Schedule id not found." };

            var isTeacherExist = await TeacherQuery().AnyAsync(x => x.Id == request.TeacherId);

            if (!isTeacherExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Teacher id not found." };


            item.ClassName = request.ClassName;
            item.StartDate = request.StartDate;
            item.EndDate = request.EndDate;
            item.Duration = request.Duration;
            item.ScheduleId = request.ScheduleId;
            item.TeacherId = request.TeacherId;

            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success,Message = "Class updated successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None,Message = "Failed. No rows were affected." };

        }
        public async Task<ServiceResponse> UpdatePatchClassAsync(UpdatePatchClassRequest request, string id)
        {
            var item = await ClassQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null) return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class not found." };

            if (!string.IsNullOrEmpty(request.ClassName)) item.ClassName = request.ClassName;

            if (request.StartDate.HasValue) item.StartDate = request.StartDate.Value;

            if (request.EndDate.HasValue) item.EndDate = request.EndDate.Value;

            if (request.Duration != 0) item.Duration = request.Duration;

            if (!string.IsNullOrEmpty(request.ScheduleId))
            {
                var isScheduleExist = await ScheduleQuery().AnyAsync(x => x.Id == request.ScheduleId);

                if (!isScheduleExist)
                    return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Schedule id not found." };

                item.ScheduleId = request.ScheduleId;
            }
            if (!string.IsNullOrEmpty(request.TeacherId))
            {
                var isTeacherExist = await TeacherQuery().AnyAsync(x => x.Id == request.TeacherId);

                if (!isTeacherExist)
                    return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Teacher id not found." };

                item.TeacherId = request.TeacherId;
            }
            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Class updated successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
    }
}
