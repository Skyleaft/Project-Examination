
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using StackExchange.Redis;
using Web.Common.Models;

namespace Web.Components.Features.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = Constant.UserProfile;
                if (userSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                var userData = JsonSerializer.Serialize(userSession);

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                new Claim(ClaimTypes.Name,userSession.NamaLengkap ?? ""),
                new Claim(ClaimTypes.Role,userSession.Role.Nama),
                new Claim(ClaimTypes.UserData,userData),
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch(Exception ex)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }
        public async Task UpdateAuthenticationState(AuthResponse userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                Constant.UserProfile = userSession.UserProfile;
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
                Constant.UserProfile = null;
                claimsPrincipal = _anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
