using FastEndpoints;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Services.RoomServices.Endpoints.Delete;

public class Endpoint : Endpoint<Guid,ServiceResponse>
{
    private readonly IRoom _repo;

    public Endpoint(IRoom examService)
    {
        _repo = examService;
    }

    public override void Configure()
    {
        Delete("/room/{Id}");
    }

    public override async Task HandleAsync(Guid Id,CancellationToken ct)
    {
        var res = await _repo.Delete(Id);
        await SendAsync(res, cancellation: ct);
    }
}