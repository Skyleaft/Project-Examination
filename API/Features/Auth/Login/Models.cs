using Domain.Users;

namespace API.Features.Auth.Login;

internal sealed class Request
{
    public string  Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
    }
}

internal sealed class Response
{
    public string Token { get; set; }
    public DateTime ValidTo { get; set; }
    public UserProfile UserProfile { get; set; } = null!;
}