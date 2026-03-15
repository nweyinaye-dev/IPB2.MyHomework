using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Grade
{
    public class GradeService
    {
        private readonly AppDbContext _dbContext;

        public GradeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<TblGrade> GradeQuery()
        {
            IQueryable<TblGrade> query = _dbContext.TblGrades
                //.AsNoTracking()
                .Where(x => x.IsDelete == false);

            return query;
        }

        public async Task<List<GradeModel>> GetAllGradeAsync(int pageNo, int pageSize)
        {
            var result = await GradeQuery()
                .OrderByDescending(x => x.GradeName)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new GradeModel
                {
                    Id = x.Id,
                    GradeName = x.GradeName,
                    FromPercent = x.FromPercent,
                    ToPercent = x.ToPercent
                }).ToListAsync();

            return result;
        }

        public async Task<ServiceResponse> SaveGradeAsync(CreateGradeRequest req)
        {
            await _dbContext.TblGrades.AddAsync(new TblGrade
            {
                Id = Guid.NewGuid().ToString(),
                GradeName = req.GradeName,
                FromPercent = req.FromPercent,
                ToPercent = req.ToPercent,
                IsDelete = false
            });

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Grade created successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> DeleteGradeAsync(string id)
        {
            var item = await GradeQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Grade not found." };

            item.IsDelete = true;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Grade deleted successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> UpdateGradeAsync(CreateGradeRequest request, string id)
        {
            var item = await GradeQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Grade not found." };

            item.GradeName = request.GradeName;
            item.FromPercent = request.FromPercent;
            item.ToPercent = request.ToPercent;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Grade updated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> UpdatePatchGradeAsync(UpdatePatchGradeRequest request, string id)
        {
            var item = await GradeQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Grade not found." };

            if (!string.IsNullOrEmpty(request.GradeName))
                item.GradeName = request.GradeName;

            if (request.FromPercent != 0)
                item.FromPercent = request.FromPercent;

            if (request.ToPercent != 0)
                item.ToPercent = request.ToPercent;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Grade updated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
    }
}
