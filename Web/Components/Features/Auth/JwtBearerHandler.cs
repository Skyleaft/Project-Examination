using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Web.Components.Features.Auth;

public class JwtBearerHandler : DelegatingHandler
{
    private readonly ProtectedSessionStorage _sessionStorage;

    public JwtBearerHandler(ProtectedSessionStorage sessionStorage, HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
        _sessionStorage = sessionStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Fetch Session
        var userSessionStorageResult = await _sessionStorage.GetAsync<AuthResponse>("UserSession");
        var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

        if (userSession.ValidTo < DateTime.Now)
        {
            await _sessionStorage.DeleteAsync("UserSession");
            userSession = null;
        }
        
        if (!string.IsNullOrEmpty(userSession.Token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userSession.Token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}