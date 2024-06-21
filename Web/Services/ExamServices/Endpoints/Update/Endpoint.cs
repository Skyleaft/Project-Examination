﻿using FastEndpoints;
using Shared.BankSoal;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Services.ExamServices.Endpoints.Update;

public class Endpoint : Endpoint<Exam,ServiceResponse>
{
    private readonly IExam _examService;

    public Endpoint(IExam examService)
    {
        _examService = examService;
    }

    public override void Configure()
    {
        Put("/exam/{Id}");
    }

    public override async Task HandleAsync(Exam r,CancellationToken ct)
    {
        var res = await _examService.Update(r);
        await SendAsync(res, cancellation: ct);
    }
}