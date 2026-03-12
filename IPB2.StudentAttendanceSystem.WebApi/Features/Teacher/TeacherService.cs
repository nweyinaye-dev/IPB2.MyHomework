using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Teacher
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _dbContext;

        public TeacherService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<TblTeacher> TeacherQuery()
        {
            IQueryable<TblTeacher> query = _dbContext.TblTeachers
                //.AsNoTracking()
                .Where(x => x.IsDelete == false);

            return query;
        }

        public async Task<List<TeacherModel>> GetAllTeacherAsync(int pageNo, int pageSize)
        {
            var result = await TeacherQuery()
                .OrderByDescending(x => x.TeacherName)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new TeacherModel
                {
                    Id = x.Id,
                    TeacherName = x.TeacherName,
                    TeacherPhoneno = x.TeacherPhoneno,
                    TeacherAddress = x.TeacherAddress
                })
                .ToListAsync();

            return result;
        }

        public async Task<ServiceResponse> SaveTeacherAsync(CreateTeacherRequest req)
        {
            await _dbContext.TblTeachers.AddAsync(new TblTeacher
            {
                Id = Guid.NewGuid().ToString(),
                TeacherName = req.TeacherName,
                TeacherPhoneno = req.TeacherPhoneno,
                TeacherAddress = req.TeacherAddress,
                IsDelete = false
            });

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Teacher created successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> DeleteTeacherAsync(string id)
        {
            var item = await TeacherQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = "Teacher not found."
                };

            item.IsDelete = true;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Teacher deleted successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> UpdateTeacherAsync(CreateTeacherRequest request, string id)
        {
            var item = await TeacherQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = "Teacher not found."
                };

            item.TeacherName = request.TeacherName;
            item.TeacherPhoneno = request.TeacherPhoneno;
            item.TeacherAddress = request.TeacherAddress;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Teacher updated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> UpdatePatchTeacherAsync(UpdatePatchTeacherRequest request, string id)
        {
            var item = await TeacherQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = "Teacher not found."
                };

            if (!string.IsNullOrEmpty(request.TeacherName))
                item.TeacherName = request.TeacherName;

            if (!string.IsNullOrEmpty(request.TeacherPhoneno))
                item.TeacherPhoneno = request.TeacherPhoneno;

            if (!string.IsNullOrEmpty(request.TeacherAddress))
                item.TeacherAddress = request.TeacherAddress;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Teacher updated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
    }
}