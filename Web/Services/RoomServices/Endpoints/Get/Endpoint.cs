using FastEndpoints;
using Shared.RoomSet;
using Web.Client.Interfaces;

namespace Web.Services.RoomServices.Endpoints.Get;

public class Endpoint : EndpointWithoutRequest<Room>
{
    private readonly IRoom _repo;

    public Endpoint(IRoom examService)
    {
        _repo = examService;
    }

    public override void Configure()
    {
        Get("/room/{Id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await _repo.Get(Route<Guid>("Id"));
        await SendAsync(res, cancellation: ct);
    }
}