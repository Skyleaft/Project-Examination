using Shared.Common;
using Shared.Report;

namespace Web.Client.Interfaces;

public interface IReport
{
    public Task<ExamReport> Get(Guid Id);
    public Task<PaginatedResponse<ExamReport>> Find(FindRequest r, CancellationToken ct);
}