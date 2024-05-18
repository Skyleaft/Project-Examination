using API.Common;
using Domain.User;

namespace API.Features.Users.Find;

internal sealed class Request : FindRequest
{
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
    }
}

internal sealed class Response : PaginatedResponse<UserProfile>
{
}