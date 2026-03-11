using Azure.Core;
using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Schedule
{
    public class ScheduleService : IScheduleService
    {
        private readonly AppDbContext _dbContext;

        public ScheduleService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IQueryable<TblSchedule> ScheduleQuery()
        {
            IQueryable<TblSchedule> query = _dbContext.TblSchedules
                  //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        public  async Task<List<ScheduleModel>> GetAllScheduleAsync(int pageNo,int pageSize)
        {
            var result = await ScheduleQuery()
                .OrderByDescending(x => x.ScheduleName)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ScheduleModel
                {
                    Id = x.Id,
                    ScheduleName = x.ScheduleName,
                    ScheduleDays = x.ScheduleDays,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime
                }).ToListAsync();            
            return result;
        }
        public  async Task<ResponseTypes> SaveScheduleAsync(CreateScheduleRequest req)
        {
            await _dbContext.TblSchedules.AddAsync(new TblSchedule
            {
                Id = Guid.NewGuid().ToString(),
                ScheduleName = req.ScheduleName,
                ScheduleDays = req.ScheduleDays,
                StartTime = req.StartTime,
                EndTime = req.EndTime,
                IsDelete = false
            });

            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0 ? ResponseTypes.Success : ResponseTypes.None;
        }
        public async Task<ResponseTypes> DeleteScheduleAsync(string id)
        {
            var item = await ScheduleQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (item is null) return ResponseTypes.NotFound;
           // else if (item.IsDelete) return ResponseTypes.AlreadyDeleted;
           item.IsDelete = true;
           int rowAffected = await _dbContext.SaveChangesAsync();
           return rowAffected > 0 ? ResponseTypes.Success : ResponseTypes.None;
        }
        public async Task<ResponseTypes> UpdateScheduleAsync(CreateScheduleRequest request, string id)
        {
            var item = await ScheduleQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null) return ResponseTypes.NotFound;

            item.ScheduleName = request.ScheduleName;
            item.ScheduleDays = request.ScheduleDays;
            item.StartTime = request.StartTime;
            item.EndTime = request.EndTime;

            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0 ? ResponseTypes.Success : ResponseTypes.None;

        }
        public async Task<ResponseTypes> UpdatePatchScheduleAsync(UpdatePatchScheduleRequest request,string id)
        {
            var item = await ScheduleQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null) return ResponseTypes.NotFound;

            if (!string.IsNullOrEmpty(request.ScheduleName))
            {
                item.ScheduleName = request.ScheduleName;
            }
            if (!string.IsNullOrEmpty(request.ScheduleDays))
            {
                item.ScheduleDays = request.ScheduleDays;
            }
            if (!string.IsNullOrEmpty(request.StartTime))
            {
                item.StartTime = request.StartTime;
            }
            if (!string.IsNullOrEmpty(request.EndTime))
            {
                item.EndTime = request.EndTime;
            }
            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0 ? ResponseTypes.Success : ResponseTypes.None;
        }
    }
}
