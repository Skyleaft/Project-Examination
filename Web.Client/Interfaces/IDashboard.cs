using CoreLib.Dashboards;
using CsvHelper;

namespace Web.Client.Interfaces;

public interface IDashboard
{
    public Task<DashboardData> Get(CancellationToken ct,string? UserId = "");
    public Task<DosenDashboardData> GetDosen(CancellationToken ct, string? UserId = "");
}