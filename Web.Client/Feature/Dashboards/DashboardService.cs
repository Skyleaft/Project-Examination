using System.Net.Http.Json;
using CoreLib.Dashboards;
using Web.Client.Interfaces;

namespace Web.Client.Feature.Dashboards;

public class DashboardService(HttpClient _httpClient) :IDashboard
{
    public async Task<DashboardData> Get(CancellationToken ct, string? UserId = "")
    {
        var data = await _httpClient.GetFromJsonAsync<DashboardData>($"/api/DashboardData", cancellationToken: ct);
        return data;
    }

    public async Task<DosenDashboardData> GetDosen(CancellationToken ct, string? UserId = "")
    {
        var data = await _httpClient.GetFromJsonAsync<DosenDashboardData>($"/api/DashboardDataDosen", cancellationToken: ct);
        return data;
    }
}