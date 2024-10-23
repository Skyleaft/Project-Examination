using CoreLib.BankSoal;
using CoreLib.Common;
using Microsoft.EntityFrameworkCore;
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

    public async Task<CreatedResponse<Exam>> Create(Exam r, CancellationToken ct)
    {
        r.CreatedOn = DateTime.UtcNow;
        var created = _dbContext.Exam.Add(r);
        await _dbContext.SaveChangesAsync(ct);
        return new CreatedResponse<Exam>(true, "Data Berhasil Ditambahkan", created.Entity);
    }

    public async Task<ServiceResponse> Update(Exam r, CancellationToken ct)
    {
        var exam = await Get(r.Id);
        if (exam == null) return new ServiceResponse(false, "data tidak ditemukan");

        r.LastModifiedOn = DateTime.UtcNow;
        var entry = _dbContext.Entry(exam);
        entry.CurrentValues.SetValues(r);
        entry.Property(x => x.CreatedOn).IsModified = false;
        entry.Property(x => x.LastModifiedOn).IsModified = true;
        entry.Property(x => x.Nama).IsModified = true;
        entry.Property(x => x.Thumbnail).IsModified = true;
        

        var deletedSoal = entry.Entity.Soals.Except(r.Soals).ToList();
        var addedSoal = r.Soals.Except(entry.Entity.Soals).ToList();
        var updatedSoal = entry.Entity.Soals.Intersect(r.Soals).ToList();

        foreach (var updated in updatedSoal)
        {
            var entrySoal = _dbContext.Entry(updated);
            entrySoal.CurrentValues.SetValues(updated);
            entrySoal.Property(x => x.ExamId).IsModified = false;
            entrySoal.Property(x => x.Pertanyaan).IsModified = true;
            entrySoal.Property(x => x.Nomor).IsModified = true;
            entrySoal.Property(x => x.isMultipleJawaban).IsModified = true;
        }
        foreach (var deleted in deletedSoal)
        {
            _dbContext.Soal.Remove(deleted);
        }
        foreach (var added in addedSoal)
        {
            _dbContext.Soal.Add(added);
        }

        

        // // Delete children
        // foreach (var existingChild in exam.Soals)
        // {
        //     if (!r.Soals.Any(c => c.Id == existingChild.Id))
        //         _dbContext.Soal.Remove(existingChild);
        //     if (existingChild.PilihanJawaban != null) 
        //         _dbContext.SoalJawaban.RemoveRange(existingChild.PilihanJawaban);
        // }
        //
        // // Update and Insert children
        // foreach (var soal in r.Soals)
        // {
        //     var existingChild = exam.Soals
        //         .FirstOrDefault(x => x.Id == soal.Id);
        //
        //     if (existingChild != null)
        //     {
        //         // Update child
        //         _dbContext.Entry(existingChild).CurrentValues.SetValues(soal);
        //         if (soal.PilihanJawaban != null) 
        //             _dbContext.SoalJawaban.AddRange(soal.PilihanJawaban);
        //     }
        //
        //     else
        //     {
        //         _dbContext.Soal.Add(soal);
        //         if (soal.PilihanJawaban != null) 
        //             _dbContext.SoalJawaban.AddRange(soal.PilihanJawaban);
        //     }
        // }

        await _dbContext.SaveChangesAsync(ct);
        return new ServiceResponse(true, "data berhasil diupdate");
    }

    public async Task<ServiceResponse> Delete(int Id)
    {
        var find = _dbContext.Exam.Find(Id);
        if (find == null) return new ServiceResponse(false, "data tidak ditemukan");

        _dbContext.Exam.Remove(find);
        await _dbContext.SaveChangesAsync();
        return new ServiceResponse(true, "data berhasil dihapus");
    }

    public async Task<Exam> Get(int Id)
    {
        var find = _dbContext
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
            .OrderByDescending(x => x.CreatedOn)
            .ToPaginatedList(r.Page, r.PageSize, r.OrderBy, r.Direction, ct);
        return data;
    }
}