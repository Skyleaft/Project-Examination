using FastEndpoints;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Services.ExamServices.Endpoints.Delete;

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