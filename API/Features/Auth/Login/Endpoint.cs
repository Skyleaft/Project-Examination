using API.Database;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Auth.Login;

internal sealed class Endpoint : Endpoint<Request, Response>
{
    private readonly GenericRepository repo;
    private readonly IConfiguration  configuration;
    public Endpoint(GenericRepository  genericRepository,IConfiguration configuration)
    {
        this.repo = genericRepository;
        this.configuration = configuration;
    }
    public override void Configure()
    {
        Post("Auth");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        var user =repo
            .UserAccount
            .FirstOrDefault(x => x.Username == r.Username);
        if (user == null)
            ThrowError("Username atau Email Tidak Ditemukan");

        if (user.Password != r.Password)
            ThrowError("Password Salah");
        
        if(!user.IsActive)
            ThrowError("Akun Tidak Aktif");

        string clientDeviceInfo = HttpContext.Request.Headers["User-Agent"];
        string ipAddress = HttpContext.Request.Headers["X-Forwarded-For"];
        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
        }

        user.Device = clientDeviceInfo;
        user.IPAddress = ipAddress;
        await repo.SaveChangesAsync(c);
        

        var res = new Response();
        res.UserProfile = repo
            .UserProfile
            .Include(x=>x.Role)
            .AsNoTracking()
            .FirstOrDefault(x => x.UserAccountId == user.Id);
        var validto =DateTime.UtcNow.AddDays(1);
        var jwtToken = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = configuration["Auth:SigningKey"];
                o.ExpireAt = validto;
                o.User.Roles.Add(res.UserProfile.Role.Nama, res.UserProfile.Role.Level.ToString());
                o.User.Claims.Add(("Username", r.Username));
                o.User.Claims.Add(("Name", res.UserProfile.NamaLengkap));
                o.User["UserProfileId"] = res.UserProfile.Id.ToString();
            });
        res.Token = jwtToken;
        res.ValidTo = validto;
        await SendAsync(res);
    }
}