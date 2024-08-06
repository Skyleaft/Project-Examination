using CoreLib.Common;
using CoreLib.Report;
using CoreLib.RoomSet;

namespace Web.Services.ReportServices.Endpoints.Find;

public class Endpoint : Endpoint<FindRequest, PaginatedResponse<ExamReport>>
{
    private readonly IReport _repo;

    public Endpoint(IReport repo)
    {
        _repo = repo;
    }

    public override void Configure()
    {
        Post("/report/Find");
        ResponseCache(60);
    }

    public override async Task HandleAsync(FindRequest r, CancellationToken ct)
    {
        var res = await _repo.Find(r, ct);
        await SendAsync(res, cancellation: ct);
    }
}