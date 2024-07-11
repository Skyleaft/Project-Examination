using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Report;
using Web.Client.Interfaces;
using Web.Common.Database;

namespace Web.Services.ReportServices;

public class ReportService(AppDbContext _dbContext) : IReport
{
    public async Task<ExamReport> Get(Guid Id)
    {
        var find = await _dbContext
            .ExamReport
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == Id);
        if (find == null) return null;

        return find;
    }

    public async Task<PaginatedResponse<ExamReport>> Find(FindRequest r, CancellationToken ct)
    {
        var data = await _dbContext
            .ExamReport
            .WhereIf(!string.IsNullOrEmpty(r.Search),
                x => x.NamaLengkap.ToLower().Contains(r.Search.ToLower()) ||
                     x.AsalKota.ToLower().Contains(r.Search.ToLower()) ||
                     x.NamaRoom.ToLower().Contains(r.Search.ToLower())
            )
            .OrderBy(x => x.Jadwal)
            .ToPaginatedListAsync(r.Page, r.PageSize, r.OrderBy, r.Direction, ct);
        return data;
    }
}