using API.Common;
using Domain.User;

namespace API.Features.Users.Delete;

internal sealed class Request
{
    public int Id { get; set; }
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
