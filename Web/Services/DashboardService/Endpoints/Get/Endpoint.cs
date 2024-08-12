using System.Security.Claims;
using CoreLib.Dashboards;
using CoreLib.TakeExam;

namespace Web.Services.DashboardService.Endpoints.Get;

public class Endpoint : EndpointWithoutRequest<DashboardData>
{
    private readonly IDashboard _repo;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Endpoint(IDashboard repo, IHttpContextAccessor httpContextAccessor)
    {
        _repo = repo;
        _httpContextAccessor = httpContextAccessor;
    }

    public override void Configure()
    {
        Get("/DashboardData");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var res = await _repo.Get(ct,userID);
        await SendAsync(res, cancellation: ct);
    }
}