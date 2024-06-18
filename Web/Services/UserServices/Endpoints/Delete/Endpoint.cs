using FastEndpoints;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Services.UserServices.Endpoints.Delete;

public class Endpoint(IUser repo) : Endpoint<string, ServiceResponse>
{
    public override void Configure()
    {
        Delete("/user/{Id}");
    }

    public override async Task HandleAsync(string Id,CancellationToken ct)
    {
        var res = await repo.Delete(Id,ct);
        await SendAsync(res, cancellation: ct);
    }
}