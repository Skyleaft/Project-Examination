using Domain.Users;
using FluentValidation;

namespace API.Features.Users.Edit;

internal sealed class Request : UserProfile
{
}

internal sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(a => a.UserAccount.Username).NotEmpty().NotNull();
        RuleFor(a => a.UserAccount.Password).NotEmpty().NotNull();
    }
}
