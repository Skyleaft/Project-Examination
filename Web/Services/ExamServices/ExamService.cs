using Microsoft.EntityFrameworkCore;
using Shared.BankSoal;
using Shared.Common;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;
using Web.Common.Database;

namespace Web.Services.ExamServices;

public class ExamService : IExam
{
    private readonly AppDbContext _dbContext;

    public ExamService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreatedResponse<Exam>> Create(Exam r)
    {
        r.CreatedOn = DateTime.UtcNow;
        var created =  _dbContext.Exam.Add(r);
         _dbContext.SaveChanges();
        return new CreatedResponse<Exam>(true, "Data Berhasil Ditambahkan", created.Entity);
    }

    public async Task<ServiceResponse> Update(Exam r)
    {
        var exam = await Get(r.Id);
        if (exam == null) return new ServiceResponse(false, "data tidak ditemukan");

        r.LastModifiedOn = DateTime.UtcNow;
        _dbContext.Entry(exam).CurrentValues.SetValues(r);

        // Delete children
        foreach (var existingChild in exam.Soals)
        {
            if (!r.Soals.Any(c => c.Id == existingChild.Id))
                _dbContext.Soal.Remove(existingChild);
            _dbContext.SoalJawaban.RemoveRange(existingChild.PilihanJawaban);
        }

        // Update and Insert children
        foreach (var soal in r.Soals)
        {
            var existingChild = exam.Soals
                .FirstOrDefault(x => x.Id == soal.Id);

            if (existingChild != null)
            {
                // Update child
                _dbContext.Entry(existingChild).CurrentValues.SetValues(soal);
                 _dbContext.SoalJawaban.AddRange(soal.PilihanJawaban);
            }

            else
            {
                 _dbContext.Soal.Add(soal);
                 _dbContext.SoalJawaban.AddRange(soal.PilihanJawaban);
            }
        }

         _dbContext.SaveChanges();
        return new ServiceResponse(true, "data berhasil diupdate");
    }

    public async Task<ServiceResponse> Delete(int Id)
    {
        var find = _dbContext.Exam.Find(Id);
        if (find == null) return new ServiceResponse(false, "data tidak ditemukan");

        _dbContext.Exam.Remove(find); 
        _dbContext.SaveChanges();
        return new ServiceResponse(true, "data berhasil dihapus");
    }

    public async Task<Exam> Get(int Id)
    {
        var find =  _dbContext
            .Exam
            .Include(x => x.Soals.OrderBy(s => s.Nomor))
            .ThenInclude(xs => xs.PilihanJawaban)
            .FirstOrDefault(x => x.Id == Id);
        if (find == null) return null;

        return find;
    }

    public async Task<PaginatedResponse<Exam>> Find(FindRequest r, CancellationToken ct)
    {
        var data = await _dbContext.Exam.WhereIf(!string.IsNullOrEmpty(r.Search),
                x => x.Nama.ToLower()
                    .Contains(r.Search.ToLower()))
            .OrderBy(x => x.Id)
            .ToPaginatedList(r.Page, r.PageSize, r.OrderBy, r.Direction, ct);
        return data;
    }
}