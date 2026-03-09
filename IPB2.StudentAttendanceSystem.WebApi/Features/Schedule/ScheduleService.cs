using IPB2.EFCore.Database.AppDbContextModels;
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
            IQueryable<TblSchedule> query = _dbContext.TblSchedules.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        public  async Task<List<ScheduleModel>> GetAllScheduleAsync()
        {
            var result = await ScheduleQuery().Select(x => new ScheduleModel
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
