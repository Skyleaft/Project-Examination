using Shared.BankSoal;
using Shared.Common;
using Web.Client.Shared.Models;
using Web.Components.Features.UserManagement;

namespace Web.Services.ExamService;

public interface IExam
{
    public Task<ServiceResponse> Create(Exam r);
    public Task<ServiceResponse> Update(Exam r);
    public Task<ServiceResponse> Delete(int Id);
    public Task<Exam> Get(int Id);
    public Task<PaginatedResponse<Exam>> Find(FindRequest r,CancellationToken ct);
}