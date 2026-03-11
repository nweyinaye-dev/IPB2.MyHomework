using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Class
{
    public class ClassService : IClassService
    {
        private readonly AppDbContext _dbContext;

        public ClassService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IQueryable<TblClass> ClassQuery()
        {
            IQueryable<TblClass> query = _dbContext.TblClasses
                   //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        public async Task<List<ClassModel>> GetAllClassAsync(int pageNo, int pageSize)
        {
            var result = await ClassQuery()
                .OrderByDescending(x => x.ClassName)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ClassModel
                {
                    Id = x.Id,
                    ClassName = x.ClassName,
                    StartDate = x.StartDate,
                    Duration = x.Duration,
                    ScheduleId = x.ScheduleId,
                    TeacherId = x.TeacherId
                }).ToListAsync();
            return result;
        }
        public async Task<ResponseTypes> SaveClassAsync(CreateClassRequest req)
        {
            await _dbContext.TblClasses.AddAsync(new TblClass
            {
                Id = Guid.NewGuid().ToString(),
                ClassName = req.ClassName,
                StartDate = req.StartDate,
                Duration = req.Duration,
                ScheduleId = req.ScheduleId,
                TeacherId = req.TeacherId,
                IsDelete = false
            });

            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0 ? ResponseTypes.Success : ResponseTypes.None;
        }
        public async Task<ResponseTypes> DeleteClassAsync(string id)
        {
            var item = await ClassQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (item is null) return ResponseTypes.NotFound;
            // else if (item.IsDelete) return ResponseTypes.AlreadyDeleted;
            item.IsDelete = true;
            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0 ? ResponseTypes.Success : ResponseTypes.None;
        }
        public async Task<ResponseTypes> UpdateClassAsync(CreateClassRequest request, string id)
        {
            var item = await ClassQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null) return ResponseTypes.NotFound;

            item.ClassName = request.ClassName;
            item.StartDate = request.StartDate;
            item.Duration = request.Duration;
            item.ScheduleId = request.ScheduleId;
            item.TeacherId = request.TeacherId;

            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0 ? ResponseTypes.Success : ResponseTypes.None;

        }
        public async Task<ResponseTypes> UpdatePatchClassAsync(UpdatePatchClassRequest request, string id)
        {
            var item = await ClassQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null) return ResponseTypes.NotFound;

            if (!string.IsNullOrEmpty(request.ClassName))
            {
                item.ClassName = request.ClassName;
            }
            if (!string.IsNullOrEmpty(request.StartDate))
            {
                item.StartDate = request.StartDate;
            }
            if (request.Duration != 0)
            {
                item.Duration = request.Duration;
            }
            if (!string.IsNullOrEmpty(request.ScheduleId))
            {
                item.ScheduleId = request.ScheduleId;
            }
            if (!string.IsNullOrEmpty(request.TeacherId))
            {
                item.TeacherId = request.TeacherId;
            }
            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0 ? ResponseTypes.Success : ResponseTypes.None;
        }
    }
}
