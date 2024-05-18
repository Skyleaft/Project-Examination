using API.Database;

namespace Features.Auth.Login;

internal sealed class Endpoint : Endpoint<Request, Response>
{
    public readonly GenericRepository genericRepository;
    public Endpoint(GenericRepository  genericRepository)
    {
        this.genericRepository = genericRepository;
    }
    public override void Configure()
    {
        Post("Auth");
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        var user =genericRepository.UserAccount.FirstOrDefault(x => x.Username == r.Username);
        if (user == null)
            await SendNotFoundAsync(c);

        if (user.Password != r.Password)
            await SendUnauthorizedAsync(c);

        string clientDeviceInfo = HttpContext.Request.Headers["User-Agent"];
        string ipAddress = HttpContext.Request.Headers["X-Forwarded-For"];
        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
        }

        user.Device = clientDeviceInfo;
        user.IPAddress = ipAddress;

        var res = new Response();
        res.UserProfile = genericRepository
            .UserProfile
            .FirstOrDefault(x => x.UserAccountId == user.Id);
        await SendAsync(new Response());
    }
}