using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public class User :IdentityUser
{
    public string? Initials { get; set; }
}