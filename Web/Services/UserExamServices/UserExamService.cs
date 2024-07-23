using System.Linq.Dynamic.Core;
using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.TakeExam;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;
using Web.Common.Database;

namespace Web.Services.UserExamServices;

public class UserExamService : IUserExam
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly AppDbContext _dbContext;

    public UserExamService(AuthenticationStateProvider authenticationStateProvider, AppDbContext dbContext)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _dbContext = dbContext;
    }

    public async Task<CreatedResponse<UserExam>> Create(CreateUserExamDTO r)
    {
        var check =  _dbContext.UserExam.AsNoTracking()
            .FirstOrDefault(x => x.UserId == r.UserId && x.RoomId == r.RoomId);
        if (check != null)
            return new CreatedResponse<UserExam>(false, "(Duplicated) Anda Sudah Berada Diruangan Tersebut");
        var data = r.Adapt<UserExam>();
        var created =  _dbContext.UserExam.Add(data); 
        _dbContext.SaveChanges();
        return new CreatedResponse<UserExam>(true, "Data Berhasil Ditambahkan", created.Entity);
    }

    public async Task<ServiceResponse> Update(UserExam r)
    {
        var userExam = await Get(r.Id);
        if (userExam == null) return new ServiceResponse(false, "data tidak ditemukan");
        
        var entry = _dbContext.Entry(userExam);
        entry.Entity.EndDate = r.EndDate;
        entry.Entity.StartDate = r.StartDate;
        entry.Entity.RoomId = r.RoomId;
        entry.Entity.UserId = r.UserId;
        entry.Entity.IsDone =r.IsDone;
        entry.Entity.IsOngoing = r.IsOngoing;
        entry.Entity.TimeLeft = r.TimeLeft;
        
        // if (r.UserAnswers != null)
        // {
        //     var existingAnswers = userExam.UserAnswers.ToList();
        //     var updatedAnswers = r.UserAnswers.Where(a => existingAnswers.Any(ea => ea.Id == a.Id)).ToList();
        //     var newAnswers = r.UserAnswers.Where(a => !existingAnswers.Any(ea => ea.Id == a.Id)).ToList();
        //
        //     _dbContext.UserAnswer.UpdateRange(updatedAnswers);
        //     _dbContext.UserAnswer.AddRange(newAnswers);
        //
        //     var answersToRemove = existingAnswers.Where(ea => !r.UserAnswers.Any(a => a.Id == ea.Id)).ToList();
        //     _dbContext.UserAnswer.RemoveRange(answersToRemove);
        // }
        
        
        // _dbContext.Entry(userExam).CurrentValues.SetValues(r);
        //
        // Delete children
        if (userExam.UserAnswers != null)
        {
            foreach (var existingChild in userExam.UserAnswers)
                if (r.UserAnswers != null && r.UserAnswers.All(c => c.Id != existingChild.Id))
                    _dbContext.UserAnswer.Remove(existingChild);
        
            // Update and Insert children
            if (r.UserAnswers != null)
                foreach (var answer in r.UserAnswers)
                {
                    var existingChild = userExam.UserAnswers
                        .FirstOrDefault(x => x.Id == answer.Id);
        
                    if (existingChild != null)
                        // Update child
                        _dbContext.Entry(existingChild).CurrentValues.SetValues(answer);
        
                    else
                        _dbContext.UserAnswer.Add(answer);
                }
        }
        _dbContext.SaveChanges();
        return new ServiceResponse(true, "data berhasil diupdate");
    }

    public async Task<ServiceResponse> Delete(Guid Id)
    {
        var find = await _dbContext.UserExam.FindAsync(Id);
        if (find == null) return new ServiceResponse(false, "data tidak ditemukan");

        _dbContext.UserExam.Remove(find); 
         _dbContext.SaveChanges();
        return new ServiceResponse(true, "data berhasil dihapus");
    }

    public async Task<UserExam> Get(Guid Id)
    {
        var find = _dbContext
            .UserExam
            .AsSplitQuery()
            .Where(x => x.Id == Id)
            .Include(x => x.UserAnswers)!
            .ThenInclude(y => y.SoalJawaban)
            .Include(x => x.UserAnswers)!
            .ThenInclude(y => y.Soal)
            .ThenInclude(d => d.PilihanJawaban)
            .FirstOrDefault();
        if (find == null) return null;

        return find;
    }

    public async Task<UserExam> GetOnly(Guid Id)
    {
        var find = _dbContext
            .UserExam
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == Id);
        if (find == null) return null;

        return find;
    }

    public async Task<PaginatedResponse<UserExam>> Find(FindRequest r, CancellationToken ct, string? UserId = "")
    {
        if (string.IsNullOrEmpty(UserId))
        {
            var auth = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = auth.User;
            UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        var data = await _dbContext
            .UserExam
            .AsNoTracking()
            .Include(x => x.UserAnswers)!
            .ThenInclude(y => y.SoalJawaban)
            .Include(x => x.UserAnswers)!
            .ThenInclude(y => y.Soal)
            .Where(x => x.UserId == UserId)
            .OrderBy(x => x.StartDate)
            .ToPaginatedList(r.Page, r.PageSize, r.OrderBy, r.Direction, ct);
        return data;
    }

    public async Task<PaginatedResponse<UserExam>> FindReport(FindRequest r, CancellationToken ct)
    {
        var data = await _dbContext
            .UserExam
            .Include(x => x.User)
            .ThenInclude(y => y.Kota)
            .Include(x => x.UserAnswers)
            .ThenInclude(y => y.SoalJawaban)
            .Include(x => x.UserAnswers)
            .ThenInclude(y => y.Soal)
            .ToPaginatedList(r.Page, r.PageSize, r.OrderBy, r.Direction, ct);
        return data;
    }

    public async Task<bool> SaveTimeLeft(Guid Id, TimeSpan timeLeft)
    {
        var data = _dbContext.UserExam.AsNoTracking()
            .FirstOrDefault(x => x.Id == Id);
        if (data == null)
            return false;
        data.TimeLeft = timeLeft; 
        _dbContext.SaveChanges();
        return true;
    }

    public async Task<ServiceResponse> UpdateJawaban(UpdateJawabanDTO r)
    {
        var data = _dbContext.UserAnswer.FirstOrDefault(x => x.Id == r.UserAnswerId);
        if (data == null)
                return new ServiceResponse(false, "data tidak ditemukan");
        var exam = _dbContext.UserExam.FirstOrDefault(x => x.Id == r.UserExamId);
        var entry = _dbContext.Entry(data);
        entry.Entity.SoalJawabanId = r.SoalJawabanId;
        
        var entryExam = _dbContext.Entry(exam);
        entryExam.Entity.TimeLeft = r.TimeLeft;
        _dbContext.SaveChanges();
        return new ServiceResponse(true, "data berhasil diupdate");
    }

    public async Task<List<UserAnswer>> GetUserAnswers(Guid UserExamId)
    {
        var data = _dbContext.UserAnswer
            .Where(x => x.UserExamId == UserExamId)
            .ToList();
        return data;
    }
}