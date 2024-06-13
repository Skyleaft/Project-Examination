using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Web.Common.Theme;
using Web.Services;

namespace Web.Components.Layout;

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
            await ApplyUserPreferences();
            await _mudThemeProvider.WatchSystemPreference(OnSystemPreferenceChanged);
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