using API.Common;
using Domain.Users;

namespace API.Features.Users.Activate;

internal sealed class Request
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
    }
}

internal sealed class Response
{
    public string Message { get; set; }
    public UserProfile DeletedUser { get; set; }
}
