using FastEndpoints;
using Shared.BankSoal;
using Web.Services.ExamService;

namespace Web.API.ExamEndpoint.Get;

public class Endpoint : Endpoint<int,Exam>
{
    private readonly IExam _examService;

    public Endpoint(IExam examService)
    {
        _examService = examService;
    }

    public override void Configure()
    {
        Get("/exam/{Id}");
    }

    public override async Task HandleAsync(int Id,CancellationToken ct)
    {
        var res = await _examService.Get(Id);
        await SendAsync(res, cancellation: ct);
    }
}