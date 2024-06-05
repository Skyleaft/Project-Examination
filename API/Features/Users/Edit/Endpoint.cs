using API.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Users.Edit;

internal sealed class Endpoint : Endpoint<Request>
{
    public readonly GenericRepository repo;
    public Endpoint(GenericRepository  genericRepository)
    {
        this.repo = genericRepository;
    }
    public override void Configure()
    {
        Put("users/{Id}");
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        var currentData = await repo.UserProfile
            .Include(x=>x.UserAccount)
            .FirstOrDefaultAsync(x=>x.Id==r.Id,c);
        if(currentData==null)
            ThrowError("User not found");
        
        r.LastModifiedOn = DateTime.Now;
        r.LastModifiedBy = HttpContext
            .User
            .Claims
            .FirstOrDefault(x=>x.Type=="Username")?.Value;
        currentData.LastModifiedBy = r.LastModifiedBy;
        currentData.LastModifiedOn = r.LastModifiedOn;
        currentData.NamaLengkap = r.NamaLengkap;
        currentData.RoleId = r.RoleId;
        currentData.NoTelp = r.NoTelp;
        currentData.Gender = r.Gender;
        currentData.TangalLahir = r.TangalLahir;
        currentData.UserAccount.Email = r.UserAccount.Email;
        currentData.UserAccount.IsActive=r.UserAccount.IsActive;
        currentData.UserAccount.Password = r.UserAccount.Password;
        await repo.SaveChangesAsync(c);
        await SendOkAsync(c);
    }
}