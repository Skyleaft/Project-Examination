using FastEndpoints;
using Web.Client.Feature.UserManagements;
using Web.Client.Interfaces;

namespace Web.Services.UserServices.Endpoints.Get;

public class Endpoint(IUser repo) : EndpointWithoutRequest<UserDTO>
{
    public override void Configure()
    {
        Get("/user/{Id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await repo.Get(Route<string>("Id"));
        await SendAsync(res, cancellation: ct);
    }
}