using Shared.Common;
using Shared.TakeExam;
using Web.Client.Shared.Models;

namespace Web.Client.Interfaces;

public interface IUserExam
{
    public Task<CreatedResponse<UserExam>> Create(CreateUserExamDTO r);
    public Task<ServiceResponse> Update(UserExam r);
    public Task<ServiceResponse> Delete(Guid Id);
    public Task<UserExam> Get(Guid Id);
    public Task<PaginatedResponse<UserExam>> Find(FindRequest r,CancellationToken ct,string? UserId="");
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