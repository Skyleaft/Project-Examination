using API.Common;
using API.Database;
using Domain.Users;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Users.Find;

internal sealed class Endpoint : Endpoint<Request, PaginatedResponse<UserProfile>>
{
    public readonly GenericRepository repo;
    public Endpoint(GenericRepository  genericRepository)
    {
        this.repo = genericRepository;
    }
    public override void Configure()
    {
        Post("users/find");
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        var data = await repo
            .UserProfile
            .Include(x=>x.Role)
            .Where(x => x.NamaLengkap.Equals(r.Search))
            .ToPaginatedListAsync(r.Page, r.PageSize);
        await SendAsync(data);
    }
}