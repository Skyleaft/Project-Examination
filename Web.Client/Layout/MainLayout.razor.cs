using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using Shared.Users;
using Web.Client.Services;
using Web.Client.Shared.Theme;

namespace Web.Client.Layout;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    [CascadingParameter]
    private Task<AuthenticationState>? authenticationState { get; set; }
    [Inject] private LayoutService LayoutService { get; set; }
    [Inject] NavigationManager navManager { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] private ISessionStorageService _sessionStorageService { get; set; }
    private MudThemeProvider _mudThemeProvider;
    private NavMenu _navMenuRef;
    private bool _drawerOpen = true;
    private bool _topMenuOpen = false;
    private ApplicationUser CurrentUser;
    private string avatarSource = "";
    private bool isLoaded;
    
    protected override Task OnInitializedAsync()
    {
        LayoutService.SetBaseTheme(Themes.LandingPageTheme());
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
        LayoutService.MajorUpdateOccurred += LayoutServiceOnMajorUpdateOccured;
        return base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            isLoaded = false;
            await ApplyUserPreferences();
            await _mudThemeProvider.WatchSystemPreference(OnSystemPreferenceChanged);
            
            var authState = await AuthenticationStateProvider
                .GetAuthenticationStateAsync();
            var user = authState.User;
            // if (user.Identity is not null && user.Identity.IsAuthenticated)
            // {
            //     var userid = _userManager.GetUserId(user);
            //     CurrentUser = await _userManager.FindByIdAsync(userid);
            //     if(CurrentUser.Photo!=null)
            //         avatarSource = $"data:image;base64,{Convert.ToBase64String(CurrentUser.Photo)}";
            // }
            isLoaded = true;
            StateHasChanged();
        }
    }
    
    private void ToggleDrawer()
    {
        _drawerOpen = !_drawerOpen;
    }
    private void OpenTopMenu()
    {
        _topMenuOpen = true;
    }
    
    private void OnDrawerOpenChanged(bool value)
    {
        _topMenuOpen = false;
        _drawerOpen = value;
        StateHasChanged();
    }
    
    private async Task ApplyUserPreferences()
    {
        var defaultDarkMode = await _mudThemeProvider.GetSystemPreference();
        await LayoutService.ApplyUserPreferences(defaultDarkMode);
    }

    private async Task OnSystemPreferenceChanged(bool newValue)
    {
        await LayoutService.OnSystemPreferenceChanged(newValue);
    }

    public void Dispose()
    {
        LayoutService.MajorUpdateOccurred -= LayoutServiceOnMajorUpdateOccured;
    }
    private void LayoutServiceOnMajorUpdateOccured(object sender, EventArgs e) => StateHasChanged();
}