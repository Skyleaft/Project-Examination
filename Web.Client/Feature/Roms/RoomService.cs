using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Shared.BankSoal;
using Shared.Common;
using Shared.RoomSet;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Client.Feature.Roms;

public class RoomService(HttpClient _httpClient) : IRoom
{
    public async Task<CreatedResponse<Room>> Create(Room r)
    {
        var res = await _httpClient.PostAsJsonAsync("api/room/",r);
        if (res.IsSuccessStatusCode)
        {
            var created = await res.Content.ReadFromJsonAsync<CreatedResponse<Room>>();
            return created;
        }
        else
        {
            return new CreatedResponse<Room>(false, res.ReasonPhrase);
        }
    }

    public async Task<ServiceResponse> Update(Room r)
    {
        var res = await _httpClient.PutAsJsonAsync($"api/room/{r.Id}",r);
        if (res.IsSuccessStatusCode)
        {
            var content = await res.Content.ReadFromJsonAsync<ServiceResponse>();
            return content;
        }
        else if (res.StatusCode == HttpStatusCode.BadRequest)
        {
            var content = await res.Content.ReadFromJsonAsync<BadResponse>();
            return new ServiceResponse(false, JsonSerializer.Serialize(content.Errors));
        }
        else
        {
            return new ServiceResponse(false, res.ReasonPhrase);
        }
    }

    public async Task<ServiceResponse> Delete(Guid Id)
    {
        var res = await _httpClient.DeleteFromJsonAsync<ServiceResponse>($"api/room/{Id}");
        return res;
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