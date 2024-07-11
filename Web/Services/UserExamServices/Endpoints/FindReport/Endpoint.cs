using FastEndpoints;
using Shared.Common;
using Shared.TakeExam;
using Web.Client.Interfaces;

namespace Web.Services.UserExamServices.Endpoints.FindReport;

public class Endpoint : Endpoint<FindRequest, PaginatedResponse<UserExam>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserExam _repo;

    public Endpoint(IUserExam examService, IHttpContextAccessor httpContextAccessor)
    {
        _repo = examService;
        _httpContextAccessor = httpContextAccessor;
    }

    public override void Configure()
    {
        Post("/UserExam/FindReport");
    }

    public override async Task HandleAsync(FindRequest r, CancellationToken ct)
    {
        var res = await _repo.FindReport(r, ct);
        await SendAsync(res, cancellation: ct);
    }
}