using FastEndpoints;
using Shared.TakeExam;
using Web.Client.Interfaces;

namespace Web.Services.UserExamServices.Endpoints.GetUserAnswer;

public class Endpoint : EndpointWithoutRequest<List<UserAnswer>>
{
    private readonly IUserExam _repo;

    public Endpoint(IUserExam examService)
    {
        _repo = examService;
    }

    public override void Configure()
    {
        Get("/UserExam/{Id}/UserAnswer/");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await _repo.GetUserAnswers(Route<Guid>("Id"));
        await SendAsync(res, cancellation: ct);
    }
}