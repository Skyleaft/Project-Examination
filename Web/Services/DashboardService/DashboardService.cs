using System.Security.Claims;
using CoreLib.Dashboards;
using CoreLib.RoomSet;
using CoreLib.TakeExam;
using Mapster;
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
            .ThenInclude(y=>y.Exam)
            .AsNoTracking()
            .Where(x => x.UserId == UserId);

        var res = new DashboardData()
        {
            TotalUjian = await data.CountAsync(cancellationToken: ct),
            Avg = await data.AverageAsync(x => x.ScoreNormalizeData, cancellationToken: ct)??0,
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
}