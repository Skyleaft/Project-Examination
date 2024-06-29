using FastEndpoints;
using Shared.BankSoal;
using Shared.RoomSet;
using Web.Client.Interfaces;

namespace Web.Services.RoomServices.Endpoints.GetByKode;

public class Endpoint : EndpointWithoutRequest<Room>
{
    private readonly IRoom _repo;

    public Endpoint(IRoom examService)
    {
        _repo = examService;
    }

    public override void Configure()
    {
        Get("/room");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await _repo.Get(Query<string>("kode"));
        await SendAsync(res, cancellation: ct);
    }
}