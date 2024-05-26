using API.Database;
using Domain.Users;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Users.Get;

internal sealed class Endpoint : Endpoint<Request, UserProfile>
{
    public readonly GenericRepository repo;
    public Endpoint(GenericRepository  genericRepository)
    {
        this.repo = genericRepository;
    }
    public override void Configure()
    {
        Get("users/{Id}");
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        var data = await repo
            .UserProfile
            .Include(x=>x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == r.Id,c);
        await SendAsync(data);
    }
}