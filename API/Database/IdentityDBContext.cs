using Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Database;

public class IdentityDBContext : IdentityDbContext<User>
{
    public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
    :base(options)
    {
        
    }
}