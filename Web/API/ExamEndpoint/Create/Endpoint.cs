using FastEndpoints;
using Shared.BankSoal;
using Web.Client.Shared.Models;
using Web.Services.ExamService;

namespace Web.API.ExamEndpoint.Create;

public class Endpoint : Endpoint<Exam,ServiceResponse>
{
    private readonly IExam _examService;

    public Endpoint(IExam examService)
    {
        _examService = examService;
    }

    public override void Configure()
    {
        Post("/exam");
    }

    public override async Task HandleAsync(Exam r,CancellationToken ct)
    {
        var res = await _examService.Create(r);
        await SendAsync(res, cancellation: ct);
    }
}