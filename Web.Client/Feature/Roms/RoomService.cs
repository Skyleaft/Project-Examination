using System.Net.Http.Json;
using Shared.BankSoal;
using Shared.Common;
using Shared.RoomSet;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Client.Feature.Roms;

public class RoomService(HttpClient _httpClient) : IRoom
{
    public Task<CreatedResponse<Room>> Create(Room r)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> Update(Room r)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> Delete(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<Room> Get(Guid Id)
    {
        var data = await _httpClient.GetFromJsonAsync<Room>($"/api/room/{Id}");
        return data;
    }

    public async Task<PaginatedResponse<Room>> Find(FindRequest r, CancellationToken ct)
    {
        var res = await _httpClient.PostAsJsonAsync("api/room/find", r, ct);
        var data = new PaginatedResponse<Room>();
        if (res.IsSuccessStatusCode)
        {
            data = await res.Content.ReadFromJsonAsync<PaginatedResponse<Room>>(ct);
        }

        return data;
    }
}