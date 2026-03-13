using Azure.Core;
using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.MinimumApi.Common;
using Microsoft.EntityFrameworkCore;

namespace IPB2.StudentAttendanceSystem.MinimumApi.Features.StudentEnroll
{
    public class StudentsEnrollService : IStudentsEnrollService
    {
        private readonly AppDbContext _dbContext;

        public StudentsEnrollService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<TblStudentEnroll> StudentsEnrollQuery()
        {
            IQueryable<TblStudentEnroll> query = _dbContext.TblStudentEnrolls
                //.AsNoTracking()
                .Where(x => x.IsDelete == false);

            return query;
        }

        public async Task<List<StudentsEnrollModel>> GetAllStudentsEnrollAsync(int pageNo, int pageSize)
        {
            //var result = await StudentsEnrollQuery()
            //    .OrderByDescending(x => x.StudentName)
            //    .Skip((pageNo - 1) * pageSize)
            //    .Take(pageSize)
            //    .Select(x => new StudentsEnrollModel
            //    {
            //        Id = x.Id,
            //        StudentName = x.StudentName,
            //        StudentPhoneno = x.StudentPhoneno,
            //        StudentAge = x.StudentAge,
            //        ClassId = x.ClassId
            //    })
            //    .ToListAsync();

            var query = from stu in _dbContext.TblStudentEnrolls
                        join cls in _dbContext.TblClasses on stu.ClassId equals cls.Id
                         where stu.IsDelete == false
                        orderby stu.StudentAge ascending
                        select new StudentsEnrollModel
                        {
                            Id = stu.Id,
                            ClassName = cls.ClassName,
                            StudentName = stu.StudentName,
                            StudentPhoneno = stu.StudentPhoneno,
                            StudentAge = stu.StudentAge
                        };
            // ်for pagination logic
            var result = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return result;
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