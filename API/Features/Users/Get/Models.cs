namespace API.Features.Users.Get;

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
