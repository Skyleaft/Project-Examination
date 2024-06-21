using System.Net.Http.Json;
using Shared.BankSoal;
using Shared.Common;
using Shared.Users;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Client.Feature.BankSoal;

public class ExamService(HttpClient _httpClient) :IExam
{
    public async Task<ServiceResponse> Create(Exam r)
    {
        var res = await _httpClient.PostAsJsonAsync("api/exam/",r);
        if (res.IsSuccessStatusCode)
        {
            var createdUser = await res.Content.ReadFromJsonAsync<ServiceResponse>();
            return createdUser;
        }
        else
        {
            return new ServiceResponse(false, res.ReasonPhrase);
        }
    }

    public Task<ServiceResponse> Update(Exam r)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<Exam> Get(int Id)
    {
        var data = await _httpClient.GetFromJsonAsync<Exam>($"/api/exam/{Id}");
        return data;
    }

    public async Task<PaginatedResponse<Exam>> Find(FindRequest r, CancellationToken ct)
    {
        var res = await _httpClient.PostAsJsonAsync("api/exam/find", r,ct);
        var data = new PaginatedResponse<Exam>();
        if (res.IsSuccessStatusCode)
        {
            data = await res.Content.ReadFromJsonAsync<PaginatedResponse<Exam>>(ct);
        }
        return data;
    }
}