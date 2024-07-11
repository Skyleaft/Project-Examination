using FastEndpoints;
using Shared.Common;
using Shared.TakeExam;
using Web.Client.Interfaces;

namespace Web.Services.UserExamServices.Endpoints.Create;

public class Endpoint : Endpoint<CreateUserExamDTO, CreatedResponse<UserExam>>
{
    private readonly IUserExam _repo;

    public Endpoint(IUserExam examService)
    {
        _repo = examService;
    }

    public override void Configure()
    {
        Post("/UserExam");
    }

    public override async Task HandleAsync(CreateUserExamDTO r, CancellationToken ct)
    {
        var res = await _repo.Create(r);
        if (!res.isSuccess)
            ThrowError(res.Message);
        await SendCreatedAtAsync($"/UserExam/{res.Data.Id}", res.Data, res, cancellation: ct);
    }
}