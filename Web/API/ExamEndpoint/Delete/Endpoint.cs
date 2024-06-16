using FastEndpoints;
using Shared.BankSoal;
using Web.Client.Shared.Models;
using Web.Services.ExamService;

namespace Web.API.ExamEndpoint.Delete;

public class Endpoint : Endpoint<int,ServiceResponse>
{
    private readonly IExam _examService;

    public Endpoint(IExam examService)
    {
        _examService = examService;
    }

    public override void Configure()
    {
        Delete("/exam/{Id}");
    }

    public override async Task HandleAsync(int Id,CancellationToken ct)
    {
        var res = await _examService.Delete(Id);
        await SendAsync(res, cancellation: ct);
    }
}