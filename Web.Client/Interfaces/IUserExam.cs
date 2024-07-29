﻿using MudBlazor;
using Shared.Common;
using Shared.TakeExam;
using Web.Client.Shared.Models;

namespace Web.Client.Interfaces;

public interface IUserExam
{
    public Task<CreatedResponse<UserExam>> Create(CreateUserExamDTO r, CancellationToken ct);
    public Task<ServiceResponse> Update(UserExam r, CancellationToken ct);
    public Task<ServiceResponse> Delete(Guid Id);
    public Task<UserExam> Get(Guid Id,CancellationToken ct);
    public Task<UserExam> GetOnly(Guid Id);
    public Task<PaginatedResponse<UserExam>> Find(FindRequest r, CancellationToken ct, string? UserId = "");
    public Task<PaginatedResponse<UserExam>> FindReport(FindRequest r, CancellationToken ct);
    public Task<bool> SaveTimeLeft(Guid Id, TimeSpan timeLeft);
    public Task<ServiceResponse> UpdateJawaban(UpdateJawabanDTO r, CancellationToken ct);
    public Task<List<UserAnswer>> GetUserAnswers(Guid UserExamId);
}

public class CreateUserExamDTO
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public bool IsOngoing { get; set; }
    public bool IsDone { get; set; }
    public Guid RoomId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeSpan TimeLeft { get; set; }
    public List<UserAnswer>? UserAnswers { get; set; }
}

public class UpdateJawabanDTO
{
    public Guid UserAnswerId { get; set; }
    public Guid UserExamId { get; set; }
    public Guid? SoalJawabanId { get; set; }
    public TimeSpan TimeLeft { get; set; }

}