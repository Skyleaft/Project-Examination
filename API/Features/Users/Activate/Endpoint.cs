using API.Common;
using API.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Users.Activate;

internal sealed class Endpoint : Endpoint<Request>
{
    private readonly GenericRepository repo;
    public Endpoint(GenericRepository  genericRepository)
    {
        this.repo = genericRepository;
    }
    public override void Configure()
    {
        Get("users/{Id}/{IsActive}");
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        if(r.Id==1)
             ThrowError("You can't modify this user");
        var data = await repo
            .UserProfile
            .Include(x=>x.UserAccount)
            .FirstOrDefaultAsync(x => x.Id == r.Id,c);
        repo.UserAccount.Attach(data.UserAccount);
        data.UserAccount.IsActive = r.IsActive;
        await repo.SaveChangesAsync(c);
        
        await SendOkAsync("user Activation Success",c);
        
        
    }
}