using Domain.Users;

namespace API.Features.Users.Register;

internal sealed class Request : UserProfile
{
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
    }
}
