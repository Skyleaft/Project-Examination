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
        var check = await _dbContext.UserExam
            .FirstOrDefaultAsync(x => x.UserId == r.UserId && x.RoomId == r.RoomId);
        if (check != null)
            return new CreatedResponse<UserExam>(false, "(Duplicated) Anda Sudah Berada Dikelas Tersebut");
        var data = r.Adapt<UserExam>();
        var created = await _dbContext.UserExam.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return new CreatedResponse<UserExam>(true, "Data Berhasil Ditambahkan", created.Entity);
    }

    public async Task<ServiceResponse> Update(UserExam r)
    {
        var userExam = await Get(r.Id);
        if (userExam == null) return new ServiceResponse(false, "data tidak ditemukan");

        _dbContext.Entry(userExam).CurrentValues.SetValues(r);

        // Delete children
        foreach (var existingChild in userExam.UserAnswers)
            if (!r.UserAnswers.Any(c => c.Id == existingChild.Id))
                _dbContext.UserAnswer.Remove(existingChild);

        // Update and Insert children
        foreach (var answer in r.UserAnswers)
        {
            var existingChild = userExam.UserAnswers
                .FirstOrDefault(x => x.Id == answer.Id);

            if (existingChild != null)
                // Update child
                _dbContext.Entry(existingChild).CurrentValues.SetValues(answer);

            else
                await _dbContext.UserAnswer.AddAsync(answer);
        }

        await _dbContext.SaveChangesAsync();
        return new ServiceResponse(true, "data berhasil diupdate");
    }

    public async Task<ServiceResponse> Delete(Guid Id)
    {
        var find = await _dbContext.UserExam.FindAsync(Id);
        if (find == null) return new ServiceResponse(false, "data tidak ditemukan");

        _dbContext.UserExam.Remove(find);
        await _dbContext.SaveChangesAsync();
        return new ServiceResponse(true, "data berhasil dihapus");
    }

    public async Task<UserExam> Get(Guid Id)
    {
        var find = await _dbContext
            .UserExam
            .Include(x => x.UserAnswers)
            .ThenInclude(y => y.SoalJawaban)
            .Include(x => x.UserAnswers)
            .ThenInclude(y => y.Soal)
            .ThenInclude(d => d.PilihanJawaban)
            .FirstOrDefaultAsync(x => x.Id == Id);
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
            .Include(x => x.UserAnswers)
            .ThenInclude(y => y.SoalJawaban)
            .Include(x => x.UserAnswers)
            .ThenInclude(y => y.Soal)
            .Where(x => x.UserId == UserId)
            .OrderBy(x => x.StartDate)
            .ToPaginatedListAsync(r.Page, r.PageSize, r.OrderBy, r.Direction, ct);
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
            .ToPaginatedListAsync(r.Page, r.PageSize, r.OrderBy, r.Direction, ct);
        return data;
    }

    public async Task<bool> SaveTimeLeft(Guid Id, TimeSpan timeLeft)
    {
        var data = await _dbContext.UserExam
            .FirstOrDefaultAsync(x => x.Id == Id);
        if (data == null)
            return false;
        data.TimeLeft = timeLeft;
        await _dbContext.SaveChangesAsync();
        return true;
    }
}