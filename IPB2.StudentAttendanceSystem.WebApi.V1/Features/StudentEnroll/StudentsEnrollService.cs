using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Attendance;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.StudentEnroll
{
 
    public class StudentsEnrollService
    {
        private readonly AppDbContext _dbContext = new AppDbContext();


        SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "IPB2",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true,
        };


        private IQueryable<TblStudentEnroll> StudentsEnrollQuery()
        {
            IQueryable<TblStudentEnroll> query = _dbContext.TblStudentEnrolls
                //.AsNoTracking()
                .Where(x => x.IsDelete == false);

            return query;
        }

        public async Task<List<StudentsEnrollModel>> GetAllStudentsEnrollAsync(int pageNo, int pageSize)
        {
            using (IDbConnection db = new SqlConnection(connectionString.ConnectionString))
            {
                db.Open();
                int offset = (pageNo - 1) * pageSize;
                var sql = $@"SELECT 
                                stu.[Id],
                                cls.[ClassName],
                                stu.[StudentName],
                                stu.[StudentPhoneno],
                                stu.[StudentAge]
                            FROM [dbo].[Tbl_StudentEnroll] stu
                            INNER JOIN [dbo].[Tbl_Class] cls ON stu.[ClassId] = cls.[Id]
                            WHERE stu.[isDelete] = 0
                            ORDER BY stu.[StudentAge] ASC
                            OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";

                var result = await db.QueryAsync<StudentsEnrollModel>(sql, new { Offset = offset, PageSize = pageSize });
                return result.ToList();
            }
        }

        public async Task<ServiceResponse> SaveStudentsEnrollAsync(CreateStudentsEnrollRequest req)
        {
            var isSClassExist = await _dbContext.TblClasses.Where(x => x.IsDelete == false).AnyAsync(x => x.Id == req.ClassId);

            if (!isSClassExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };

            await _dbContext.TblStudentEnrolls.AddAsync(new TblStudentEnroll
            {
                Id = Guid.NewGuid().ToString(),
                StudentName = req.StudentName,
                StudentPhoneno = req.StudentPhoneno,
                StudentAge = req.StudentAge,
                ClassId = req.ClassId,
                IsDelete = false
            });

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Student enrollment created successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> DeleteStudentsEnrollAsync(string id)
        {
            var item = await StudentsEnrollQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = "Student enrollment not found."
                };

            item.IsDelete = true;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Student enrollment deleted successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> UpdateStudentsEnrollAsync(CreateStudentsEnrollRequest request, string id)
        {
            var item = await StudentsEnrollQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = "Student enrollment not found."
                };

            var isSClassExist = await _dbContext.TblClasses.Where(x => x.IsDelete == false).AnyAsync(x => x.Id == request.ClassId);

            if (!isSClassExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };

            item.StudentName = request.StudentName;
            item.StudentPhoneno = request.StudentPhoneno;
            item.StudentAge = request.StudentAge;
            item.ClassId = request.ClassId;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Student enrollment updated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> UpdatePatchStudentsEnrollAsync(UpdatePatchStudentsEnrollRequest request, string id)
        {
            var item = await StudentsEnrollQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Student enrollment not found."};

            if (!string.IsNullOrEmpty(request.StudentName))
                item.StudentName = request.StudentName;

            if (!string.IsNullOrEmpty(request.StudentPhoneno))
                item.StudentPhoneno = request.StudentPhoneno;

            if (request.StudentAge != 0)
                item.StudentAge = request.StudentAge;

            if (!string.IsNullOrEmpty(request.ClassId))
            {
                var isSClassExist = await _dbContext.TblClasses.Where(x => x.IsDelete == false).AnyAsync(x => x.Id == request.ClassId);

                if (!isSClassExist)
                    return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };

                item.ClassId = request.ClassId;
            }                

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Student enrollment updated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
    }
}
