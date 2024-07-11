using FastEndpoints;
using Shared.TakeExam;
using Web.Client.Interfaces;

namespace Web.Services.UserExamServices.Endpoints.Get;

public class Endpoint : EndpointWithoutRequest<UserExam>
{
    private readonly IUserExam _repo;

    public Endpoint(IUserExam examService)
    {
        _repo = examService;
    }

    public override void Configure()
    {
        Get("/UserExam/{Id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await _repo.Get(Route<Guid>("Id"));
        await SendAsync(res, cancellation: ct);
    }
}