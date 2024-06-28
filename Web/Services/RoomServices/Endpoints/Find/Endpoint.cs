using FastEndpoints;
using Shared.BankSoal;
using Shared.Common;
using Shared.RoomSet;
using Web.Client.Interfaces;

namespace Web.Services.RoomServices.Endpoints.Find;

public class Endpoint : Endpoint<FindRequest,PaginatedResponse<Room>>
{
    private readonly IRoom _repo;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Endpoint(IRoom examService, IHttpContextAccessor httpContextAccessor)
    {
        _repo = examService;
        _httpContextAccessor = httpContextAccessor;
    }

    public override void Configure()
    {
        Post("/room/Find");
    }

    public override async Task HandleAsync(FindRequest r,CancellationToken ct)
    {
        var username = _httpContextAccessor.HttpContext.User.Identity.Name;
        var res = await _repo.Find(r,ct,username);
        await SendAsync(res, cancellation: ct);
    }
}