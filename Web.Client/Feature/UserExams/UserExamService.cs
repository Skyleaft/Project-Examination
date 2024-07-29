using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Shared.Common;
using Shared.TakeExam;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Client.Feature.UserExams;

public class UserExamService(HttpClient _httpClient) : IUserExam
{
    public async Task<CreatedResponse<UserExam>> Create(CreateUserExamDTO r, CancellationToken ct)
    {
        var res = await _httpClient.PostAsJsonAsync("api/userexam/", r, cancellationToken: ct);
        if (res.IsSuccessStatusCode)
        {
            var created = await res.Content.ReadFromJsonAsync<CreatedResponse<UserExam>>(cancellationToken: ct);
            return created;
        }

        var error = await res.Content.ReadFromJsonAsync<BadResponse>(cancellationToken: ct);
        return new CreatedResponse<UserExam>(false, error.Errors["generalErrors"].FirstOrDefault());
    }

    public async Task<ServiceResponse> Update(UserExam r,CancellationToken ct)
    {
        var res = await _httpClient.PutAsJsonAsync($"api/userexam/{r.Id}", r, cancellationToken: ct);
        if (res.IsSuccessStatusCode)
        {
            var content = await res.Content.ReadFromJsonAsync<ServiceResponse>(cancellationToken: ct);
            return content;
        }

        if (res.StatusCode == HttpStatusCode.BadRequest)
        {
            var content = await res.Content.ReadFromJsonAsync<BadResponse>();
            return new ServiceResponse(false, JsonSerializer.Serialize(content.Errors));
        }

        return new ServiceResponse(false, res.ReasonPhrase);
    }

    public async Task<ServiceResponse> Delete(Guid Id)
    {
        var res = await _httpClient.DeleteFromJsonAsync<ServiceResponse>($"api/userexam/{Id}");
        return res;
    }

    public async Task<UserExam> Get(Guid Id,CancellationToken ct)
    {
        var data = await _httpClient.GetFromJsonAsync<UserExam>($"/api/userexam/{Id}", cancellationToken: ct);
        return data;
    }

    public async Task<UserExam> GetOnly(Guid Id)
    {
        var data = await _httpClient.GetFromJsonAsync<UserExam>($"/api/userexam/getonly/{Id}");
        return data;
    }

    public async Task<PaginatedResponse<UserExam>> Find(FindRequest r, CancellationToken ct, string? UserId = "")
    {
        var res = await _httpClient.PostAsJsonAsync("api/userexam/find", r, ct);
        var data = new PaginatedResponse<UserExam>();
        if (res.IsSuccessStatusCode) data = await res.Content.ReadFromJsonAsync<PaginatedResponse<UserExam>>(ct);

        return data;
    }

    public async Task<PaginatedResponse<UserExam>> FindReport(FindRequest r, CancellationToken ct)
    {
        var res = await _httpClient.PostAsJsonAsync("api/userexam/findReport", r, ct);
        var data = new PaginatedResponse<UserExam>();
        if (res.IsSuccessStatusCode) data = await res.Content.ReadFromJsonAsync<PaginatedResponse<UserExam>>(ct);

        return data;
    }

    public Task<bool> SaveTimeLeft(Guid Id, TimeSpan timeLeft)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse> UpdateJawaban(UpdateJawabanDTO r, CancellationToken ct)
    {
        var res = await _httpClient.PutAsJsonAsync($"api/userexam/userAnswer/{r.UserAnswerId}", r, cancellationToken: ct);
        if (res.IsSuccessStatusCode)
        {
            var content = await res.Content.ReadFromJsonAsync<ServiceResponse>(cancellationToken: ct);
            return content;
        }

        if (res.StatusCode == HttpStatusCode.BadRequest)
        {
            var content = await res.Content.ReadFromJsonAsync<BadResponse>(cancellationToken: ct);
            return new ServiceResponse(false, JsonSerializer.Serialize(content.Errors));
        }

        return new ServiceResponse(false, res.ReasonPhrase);
    }

    public async Task<List<UserAnswer>> GetUserAnswers(Guid UserExamId)
    {
        var data = await _httpClient.GetFromJsonAsync<List<UserAnswer>>($"/api/userexam/{UserExamId}/userAnswer");
        return data;
    }
}