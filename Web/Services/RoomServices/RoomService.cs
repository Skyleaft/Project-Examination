using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.RoomSet;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;
using Web.Common.Database;

namespace Web.Services.RoomServices;

public class RoomService : IRoom
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly AppDbContext _dbContext;

    public RoomService(AppDbContext dbContext, AuthenticationStateProvider authenticationStateProvider)
    {
        _dbContext = dbContext;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<CreatedResponse<Room>> Create(Room r)
    {
        r.CreatedOn = DateTime.Now;
        var find = await _dbContext.Room.FirstOrDefaultAsync(x => x.Kode.Equals(r.Kode));
        if (find != null)
            return new CreatedResponse<Room>(false, "Kode Ruangan Sudah Digunakan");
        var created = await _dbContext.Room.AddAsync(r);
        await _dbContext.SaveChangesAsync();
        return new CreatedResponse<Room>(true, "Data Berhasil Ditambahkan", created.Entity);
    }

    public async Task<ServiceResponse> Update(Room r)
    {
        var room = await Get(r.Id);
        if (room == null) return new ServiceResponse(false, "data tidak ditemukan");

        r.LastModifiedOn = DateTime.Now;
        _dbContext.Room.Update(r);
        //_dbContext.Entry(room).CurrentValues.SetValues(r);

        await _dbContext.SaveChangesAsync();
        return new ServiceResponse(true, "data berhasil diupdate");
    }

    public async Task<ServiceResponse> Delete(Guid Id)
    {
        var find = await _dbContext.Room.FindAsync(Id);
        if (find == null) return new ServiceResponse(false, "data tidak ditemukan");

        _dbContext.Room.Remove(find);
        await _dbContext.SaveChangesAsync();
        return new ServiceResponse(true, "data berhasil dihapus");
    }

    public async Task<Room> Get(Guid Id)
    {
        var find = await _dbContext
            .Room
            .AsNoTracking()
            .Include(x => x.Exam)
            .ThenInclude(y => y.Soals.OrderBy(o => o.Nomor))
            .ThenInclude(z => z.PilihanJawaban)
            .Include(x => x.ListPeserta)
            .FirstOrDefaultAsync(x => x.Id == Id);
        if (find == null) return null;

        return find;
    }

    public Room GetSync(Guid Id)
    {
        var find = _dbContext
            .Room
            .AsNoTracking()
            .Include(x => x.Exam)
            .ThenInclude(y => y.Soals.OrderBy(o => o.Nomor))
            .ThenInclude(z => z.PilihanJawaban)
            .Include(x => x.ListPeserta)
            .FirstOrDefault(x => x.Id == Id);
        if (find == null) return null;

        return find;
    }

    public async Task<Room> Get(string kode)
    {
        var find = await _dbContext
            .Room
            .AsNoTracking()
            .Include(x => x.Exam)
            .ThenInclude(y => y.Soals)
            .FirstOrDefaultAsync(x => x.Kode == kode);
        if (find == null) return null;

        return find;
    }

    public async Task<PaginatedResponse<Room>> Find(FindRequest r, CancellationToken ct, string? Username = "")
    {
        if (string.IsNullOrEmpty(Username))
        {
            var auth = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = auth.User;
            Username = user.Identity.Name;
        }

        var data = await _dbContext
            .Room
            .AsNoTracking()
            .WhereIf(!string.IsNullOrEmpty(r.Search),
                x => x.Nama.ToLower().Contains(r.Search.ToLower()) ||
                     x.Kode.ToLower().Contains(r.Search.ToLower())
            )
            .Where(x => x.CreatedBy == Username)
            .OrderBy(x => x.CreatedOn)
            .ToPaginatedListAsync(r.Page, r.PageSize, r.OrderBy, r.Direction, ct);
        return data;
    }

    public async Task<List<Room>> AllRefference(CancellationToken ct)
    {
        var data = await _dbContext
            .Room
            .AsNoTracking()
            .OrderBy(x => x.CreatedOn)
            .Select(x => new Room()
            {
                Id = x.Id,
                Nama = x.Nama,
                Kode = x.Kode
            })
            .ToListAsync();
        return data;
    }
}