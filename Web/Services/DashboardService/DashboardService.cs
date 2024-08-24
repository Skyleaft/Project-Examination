using System.Security.Claims;
using CoreLib.Dashboards;
using CoreLib.TakeExam;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Web.Common.Database;

namespace Web.Services.DashboardService;

public class DashboardService : IDashboard
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly AppDbContext _dbContext;

    public DashboardService(AuthenticationStateProvider authenticationStateProvider, AppDbContext dbContext)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _dbContext = dbContext;
    }


    public async Task<DashboardData> Get(CancellationToken ct, string? UserId = "")
    {
        if (string.IsNullOrEmpty(UserId))
        {
            var auth = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = auth.User;
            UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        var data = _dbContext
            .UserExam
            .Include(x => x.Room)
            .ThenInclude(y => y.Exam)
            .AsNoTracking()
            .Where(x => x.UserId == UserId);

        var res = new DashboardData()
        {
            TotalUjian = await data.CountAsync(cancellationToken: ct),
            Avg = await data.AverageAsync(x => x.ScoreNormalizeData, cancellationToken: ct) ?? 0,
            RiwayatUjian = data.Select(x => new UserExamView(x)),
            UjianBerlangsung = data.Where(x =>
                    x.Room.JadwalEnd.ToLocalTime() > DateTime.Now.ToLocalTime() &&
                    x.Room.JadwalStart.ToLocalTime() < DateTime.Now.ToLocalTime())
                .Select(x => new UserExamView(x)),
            UjianAkanDatang = data.Where(x => x.Room.JadwalStart.ToLocalTime() > DateTime.Now.ToLocalTime())
                .Select(x => new UserExamView(x))
        };

        return res;
    }

    public async Task<DosenDashboardData> GetDosen(CancellationToken ct, string? UserId = "")
    {
        if (string.IsNullOrEmpty(UserId))
        {
            var auth = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = auth.User;
            UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        var currentUser = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == UserId, cancellationToken: ct);
        var peserta =await _dbContext.UserRoles
            .AsNoTracking()
            .Where(x => x.RoleId == "c6477de3-27b8-4094-b504-48ee4954995e")
            .CountAsync(cancellationToken: ct);
        var totalSoal = await _dbContext.Exam
            .AsNoTracking()
            .CountAsync(cancellationToken: ct);
        var totalRuang = await _dbContext.Room
            .AsNoTracking()
            .Where(x => x.CreatedBy == currentUser.UserName)
            .CountAsync(cancellationToken: ct);
        
        var data = _dbContext
            .UserExam
            .Include(x=>x.User)
            .Include(x => x.Room)
            .ThenInclude(y=>y.Exam)
            .AsNoTracking()
            .Where(x => x.IsDone==true);
        
        var filter = data
            .GroupBy(x => x.Room)
            .Select(x => new
            {
                Room = x.Key,
                AvgScore = (double)x.Average(s => s.ScoreNormalizeData),
                HighestScore = (double)x.Max(s=>s.ScoreNormalizeData),
                LowestScore = (double)x.Min(s=>s.ScoreNormalizeData)
            })
            .Take(10);
        
        var res = new DosenDashboardData()
        {
            TotalPeserta = peserta,
            TotalSoal = totalSoal,
            TotalRuangan = totalRuang,
            Top10 = data.Where(s=>filter.Select(r=>r.Room).Contains(s.Room))
                .Select(x=>new NilaiUser()
                {
                    NamaPeserta = x.User.NamaLengkap,
                    Avg = filter.First(r=>r.Room==x.Room).AvgScore,
                    Highest = filter.First(r=>r.Room==x.Room).HighestScore,
                    Lowest = filter.First(r=>r.Room==x.Room).LowestScore
                })
                .OrderByDescending(r=>r.Avg)
                .Take(10),
            Bottom10 = data.Where(s=>filter.Select(r=>r.Room).Contains(s.Room))
                .Select(x=>new NilaiUser()
                {
                    NamaPeserta = x.User.NamaLengkap,
                    Avg = filter.First(r=>r.Room==x.Room).AvgScore,
                    Highest = filter.First(r=>r.Room==x.Room).HighestScore,
                    Lowest = filter.First(r=>r.Room==x.Room).LowestScore
                })
                .OrderBy(r=>r.Avg)
                .Take(10)
        };

        return res;
    }
}