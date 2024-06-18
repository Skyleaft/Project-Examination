using Shared.BankSoal;
using Shared.Common;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Client.Feature.BankSoal;

public class ExamService :IExam
{
    public Task<ServiceResponse> Create(Exam r)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> Update(Exam r)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<Exam> Get(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResponse<Exam>> Find(FindRequest r, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}