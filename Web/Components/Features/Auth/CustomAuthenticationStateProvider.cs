using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Web.Components.Features.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<AuthResponse>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                var userData = JsonSerializer.Serialize(userSession.UserProfile);

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                new Claim(ClaimTypes.Name,userSession.UserProfile.NamaLengkap ?? ""),
                new Claim(ClaimTypes.Role,userSession.UserProfile.Role.Nama),
                new Claim(ClaimTypes.UserData,userData),
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }
        public async Task<ClaimsPrincipal> UpdateAuthenticationState(AuthResponse userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);
                var userData = JsonSerializer.Serialize(userSession.UserProfile);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,userSession.UserProfile.NamaLengkap ?? ""),
                    new Claim(ClaimTypes.Role,userSession.UserProfile.Role.Nama),
                    new Claim(ClaimTypes.UserData,userData),
                }, "CustomAuth"));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            return claimsPrincipal;
        }
    }
}
