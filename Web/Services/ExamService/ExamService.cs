using Shared.BankSoal;
using Shared.Common;
using Web.Common.Database;
using Web.Common.Models;

namespace Web.Services.ExamService;

public class ExamService : IExam
{
    private readonly AppDbContext _dbContext;

    public ExamService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse> Create(Exam r)
    {
        r.CreatedOn = DateTime.Now;
        var created = await _dbContext.Exam.AddAsync(r);
        await _dbContext.SaveChangesAsync();
        return new ServiceResponse( true,"data berhasil ditambahkan" );
    }

    public async Task<ServiceResponse> Update(Exam r)
    {
        var find = await _dbContext.Exam.FindAsync(r.Id);
        if (find == null)
        {
            return new ServiceResponse(false, "data tidak ditemukan");
        }
        find.Nama = r.Nama;
        find.Soals = r.Soals;
        find.Thumbnail = r.Thumbnail;
        find.IsRandomize  = r.IsRandomize;
        find.LastModifiedBy = r.LastModifiedBy;
        find.LastModifiedOn = DateTime.Now;
        await _dbContext.SaveChangesAsync();
        return new ServiceResponse(true, "data berhasil diupdate");
    }

    public async Task<ServiceResponse> Delete(int Id)
    {
        var find = await _dbContext.Exam.FindAsync(Id);
        if (find == null)
        {
            return new ServiceResponse(false, "data tidak ditemukan");
        }
        _dbContext.Exam.Remove(find);
        await _dbContext.SaveChangesAsync();
        return new ServiceResponse(true, "data berhasil dihapus");
    }

    public async Task<Exam> Get(int Id)
    {
        var find = await _dbContext.Exam.FindAsync(Id);
        if (find == null)
        {
            return null;
        }
        return find;
    }

    public async Task<PaginatedResponse<Exam>> Find(FindRequest r,CancellationToken ct)
    {
        var data =await _dbContext.Exam.WhereIf(!string.IsNullOrEmpty(r.Search),
                x => x.Nama.ToLower()
                    .Contains(r.Search.ToLower()))
            .OrderBy(x=>x.Id)
            .ToPaginatedListAsync(r.Page, r.PageSize,r.OrderBy,r.Direction,ct);
        return data;
    }
}