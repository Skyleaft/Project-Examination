using Domain.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Web.Services;

namespace Web.Components.Layout
{
    public partial class Appbar
    {
        [Parameter] public EventCallback<MouseEventArgs> DrawerToggleCallback { get; set; }
        [Inject] private LayoutService LayoutService { get; set; }
        [Inject] NavigationManager navManager { get; set; }
        [Inject] AuthenticationStateProvider authStateProvicder { get; set; }
        [Parameter] public UserAccount? currentAccountSession { get; set; }

        private async Task Logout()
        {
            // var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvicder;
            // await customAuthStateProvider.UpdateAuthenticationState(null);
            navManager.NavigateTo("/");

        }
    }
}
