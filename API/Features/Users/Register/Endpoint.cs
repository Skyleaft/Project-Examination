using API.Database;

namespace API.Features.Users.Register;

internal sealed class Endpoint : Endpoint<Request>
{
    public readonly GenericRepository repo;
    public Endpoint(GenericRepository  genericRepository)
    {
        this.repo = genericRepository;
    }
    public override void Configure()
    {
        Post("users/register");
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        var check = repo
            .UserAccount
            .FirstOrDefault(x=>x.Username.ToLower()==r.UserAccount.Username.ToLower());
        if(check is not null)
            ThrowError("Username already exists");
        r.UserAccount.IsActive = false;
        r.CreatedOn = DateTime.Now;
        r.CreatedBy = HttpContext
            .User
            .Claims
            .FirstOrDefault(x=>x.Type=="Username")?.Value; 
        var res =await repo.UserProfile.AddAsync(r,c);
        await repo.SaveChangesAsync(c);
        await SendCreatedAtAsync($"users/{res.Entity.Id}", res.Entity,res.Entity);
    }
}