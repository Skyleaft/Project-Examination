using FastEndpoints;
using Web.Client.Feature.UserManagements;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Services.UserServices.Endpoints.Register;

public class Endpoint(IUser repo) : Endpoint<UserAddDTO, ServiceResponse>
{
    public override void Configure()
    {
        Post("/user/register");
    }

    public override async Task HandleAsync(UserAddDTO r,CancellationToken ct)
    {
        var res = await repo.Register(r,ct);
        await SendAsync(res, cancellation: ct);
    }
}