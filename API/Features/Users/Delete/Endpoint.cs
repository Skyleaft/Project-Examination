using API.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Users.Delete;

internal sealed class Endpoint : Endpoint<Request,Response>
{
    private readonly GenericRepository repo;
    public Endpoint(GenericRepository  genericRepository)
    {
        this.repo = genericRepository;
    }
    public override void Configure()
    {
        Delete("users/{Id}");
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        if(r.Id==1)
             ThrowError("You can't delete this user");
        var data = await repo
            .UserProfile
            .Include(x=>x.UserAccount)
            .FirstOrDefaultAsync(x => x.Id == r.Id,c);
        repo.UserAccount.Attach(data.UserAccount);
        repo.UserAccount.Remove(data.UserAccount);
        await repo.SaveChangesAsync(c);
        
        await SendOkAsync(new Response()
        {
            Message = "User deleted successfully",
            DeletedUser = data
        },c);
        
        
    }
}