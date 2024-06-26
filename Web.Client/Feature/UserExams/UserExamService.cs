﻿using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Shared.Common;
using Shared.RoomSet;
using Shared.TakeExam;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Client.Feature.UserExams;

public class UserExamService(HttpClient _httpClient) : IUserExam
{
    public async Task<CreatedResponse<UserExam>> Create(CreateUserExamDTO r)
    {
        var res = await _httpClient.PostAsJsonAsync("api/userexam/",r);
        if (res.IsSuccessStatusCode)
        {
            var created = await res.Content.ReadFromJsonAsync<CreatedResponse<UserExam>>();
            return created;
        }
        else
        {
            var error = await res.Content.ReadFromJsonAsync<BadResponse>();
            return new CreatedResponse<UserExam>(false, error.Errors["generalErrors"].FirstOrDefault());
        }
    }

    public async Task<ServiceResponse> Update(UserExam r)
    {
        var res = await _httpClient.PutAsJsonAsync($"api/userexam/{r.Id}",r);
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
        var res = await _httpClient.DeleteFromJsonAsync<ServiceResponse>($"api/userexam/{Id}");
        return res;
    }

    public async Task<UserExam> Get(Guid Id)
    {
        var data = await _httpClient.GetFromJsonAsync<UserExam>($"/api/userexam/{Id}");
        return data;
    }

    public async Task<PaginatedResponse<UserExam>> Find(FindRequest r, CancellationToken ct, string? UserId = "")
    {
        var res = await _httpClient.PostAsJsonAsync("api/userexam/find", r, ct);
        var data = new PaginatedResponse<UserExam>();
        if (res.IsSuccessStatusCode)
        {
            data = await res.Content.ReadFromJsonAsync<PaginatedResponse<UserExam>>(ct);
        }

        return data;
    }
}