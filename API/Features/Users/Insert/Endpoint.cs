namespace Features.Users;

internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Post("UserProfile");
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        await SendAsync(new Response());
    }
}