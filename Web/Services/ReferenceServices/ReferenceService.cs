using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Users;
using Web.Client.Interfaces;
using Web.Common.Database;

namespace Web.Services.ReferenceServices;

public class ReferenceService : IReferences
{
    private readonly AppDbContext _appDbContext;
    private MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
    private readonly ILocalStorageService _storageService;

    public ReferenceService(AppDbContext appDbContext, ILocalStorageService storageService)
    {
        _appDbContext = appDbContext;
        _storageService = storageService;
    }

    public async Task<List<Provinsi>> GetAllprovinsi()
    {
        var key = "ref-provinsi";
        var data = cache.Get<List<Provinsi>>(key);
        if (data == null)
        {
            var fetch = await _appDbContext.Provinsi.ToListAsync();
            cache.Set(key, fetch, TimeSpan.FromDays(1));
            await _storageService.SetItemAsync(key, fetch);
            data = fetch;
        }
        return data;
    }

    public async Task<List<Kota>> GetAllKota()
    {
        var key = "ref-kota";
        var data = cache.Get<List<Kota>>(key);
        if (data == null)
        {
            var fetch = await _appDbContext.Kota.ToListAsync();
            cache.Set(key, fetch, TimeSpan.FromDays(1));
            await _storageService.SetItemAsync(key, fetch);
            data = fetch;
        }
        return data;
    }
}