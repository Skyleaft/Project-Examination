using System.Net.Http.Json;
using Shared.Common;
using Shared.Users;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Client.Feature.UserManagements;

public class UserService :IUser
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginatedResponse<ApplicationUser>> Find(FindRequest r, CancellationToken ct)
    {
        var res = await _httpClient.PostAsJsonAsync("api/user/find", r,ct);
        var data = new PaginatedResponse<ApplicationUser>();
        if (res.IsSuccessStatusCode)
        {
            data = await res.Content.ReadFromJsonAsync<PaginatedResponse<ApplicationUser>>(ct);
        }
        return data;
    }

    public async Task<CreatedResponse<UserDTO>> Register(UserAddDTO r, CancellationToken? ct)
    {
        var res = await _httpClient.PostAsJsonAsync("api/user/register",r);
        if (res.IsSuccessStatusCode)
        {
            var createdUser = await res.Content.ReadFromJsonAsync<CreatedResponse<UserDTO>>();
            return createdUser;
        }
        else
        {
            return new CreatedResponse<UserDTO>(false, res.ReasonPhrase);
        }
    }

    public async Task<ServiceResponse> Update(UserEditDTO r, CancellationToken ct)
    {
        var res = await _httpClient.PutAsJsonAsync($"api/user/{r.Id.ToString()}",r);
        var data = new ServiceResponse(false,"");
        if (res.IsSuccessStatusCode)
        {
            data = await res.Content.ReadFromJsonAsync<ServiceResponse>(ct);
        }
        else
        {
            data = new ServiceResponse(false, res.ReasonPhrase);
        }
        return data;
    }

    public async Task<ServiceResponse> Delete(string id, CancellationToken? ct)
    {
        var res = await _httpClient.DeleteFromJsonAsync<ServiceResponse>($"api/user/{id}");
        return res;
    }

    public async Task<UserDTO> Get(string id)
    {
        var userDTO = await _httpClient.GetFromJsonAsync<UserDTO>($"/api/user/{id}");
        return userDTO;
    }
}