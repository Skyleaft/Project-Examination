using Domain.Users;

namespace Web.Components.Features.Auth;

public sealed class AuthRequest
{
    public string  Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public sealed class AuthResponse
{
    public string Token { get; set; }
    public DateTime ValidTo { get; set; }
    public UserProfile UserProfile { get; set; } = null!;
}