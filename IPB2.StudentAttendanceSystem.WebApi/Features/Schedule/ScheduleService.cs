using IPB2.EFCore.Database.AppDbContextModels;
using Microsoft.EntityFrameworkCore;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Schedule
{
    public class ScheduleService : IScheduleService
    {
        private readonly AppDbContext _dbContext;

        public ScheduleService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  async Task<List<ScheduleModel>> GetAllScheduleAsync()
        {
            var result = await _dbContext.TblSchedules.AsNoTracking().Where(x=> x.IsDelete == false).Select(x => new ScheduleModel
            {
                Id = x.Id,
                ScheduleName = x.ScheduleName,
                ScheduleDays = x.ScheduleDays,
                StartTime = x.StartTime,
                EndTime = x.EndTime
            }).ToListAsync();
            
            return result;
        }

        public Task SaveScheduleAsync(CreateScheduleRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
