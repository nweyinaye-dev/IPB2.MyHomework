using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Schedule
{
    public class ScheduleService
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

        public async Task<List<ScheduleModel>> GetAllScheduleAsync(int pageNo, int pageSize)
        {
            var result = await ScheduleQuery()
                .OrderByDescending(x => x.ScheduleName)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ScheduleModel
                {
                    Id = x.Id,
                    ScheduleName = x.ScheduleName,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime
                })
                .ToListAsync();

            return result;
        }

        public async Task<ServiceResponse> SaveScheduleAsync(CreateScheduleRequest req)
        {
            await _dbContext.TblSchedules.AddAsync(new TblSchedule
            {
                Id = Guid.NewGuid().ToString(),
                ScheduleName = req.ScheduleName,
                StartTime = req.StartTime,
                EndTime = req.EndTime,
                IsDelete = false
            });

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Schedule created successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> DeleteScheduleAsync(string id)
        {
            var item = await ScheduleQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = "Schedule not found."
                };

            item.IsDelete = true;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Schedule deleted successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> UpdateScheduleAsync(CreateScheduleRequest request, string id)
        {
            var item = await ScheduleQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = "Schedule not found."
                };

            item.ScheduleName = request.ScheduleName;
            item.StartTime = request.StartTime;
            item.EndTime = request.EndTime;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Schedule updated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> UpdatePatchScheduleAsync(UpdatePatchScheduleRequest request, string id)
        {
            var item = await ScheduleQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = "Schedule not found."
                };

            if (!string.IsNullOrEmpty(request.ScheduleName))
                item.ScheduleName = request.ScheduleName;

            if (!string.IsNullOrEmpty(request.StartTime))
                item.StartTime = request.StartTime;

            if (!string.IsNullOrEmpty(request.EndTime))
                item.EndTime = request.EndTime;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Schedule updated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
    }
}
