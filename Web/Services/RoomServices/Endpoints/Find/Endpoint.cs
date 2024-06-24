using FastEndpoints;
using Shared.BankSoal;
using Shared.Common;
using Shared.RoomSet;
using Web.Client.Interfaces;

namespace Web.Services.RoomServices.Endpoints.Find;

public class Endpoint : Endpoint<FindRequest,PaginatedResponse<Room>>
{
    private readonly IRoom _repo;

    public Endpoint(IRoom examService)
    {
        _repo = examService;
    }

    public override void Configure()
    {
        Post("/room/Find");
    }

    public override async Task HandleAsync(FindRequest r,CancellationToken ct)
    {
        var res = await _repo.Find(r,ct);
        await SendAsync(res, cancellation: ct);
    }
}