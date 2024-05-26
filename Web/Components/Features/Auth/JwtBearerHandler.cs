using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Web.Components.Features.Auth;

public class JwtBearerHandler : DelegatingHandler
{
    private readonly AuthenticationStateProvider authStateProvider;
    public JwtBearerHandler(AuthenticationStateProvider authStateProvider)
    {
        this.authStateProvider = authStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Fetch Session
        var authState = await authStateProvider.GetAuthenticationStateAsync();
        if (authState.User.Identity?.IsAuthenticated == true)
        {
            var claims = authState
                .User
                .Claims;
            
            var token = claims.First(c => c.Type == ClaimTypes.Authentication).Value;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            request.Headers.Clear();
        }
        
        return await base.SendAsync(request, cancellationToken);
    }
}