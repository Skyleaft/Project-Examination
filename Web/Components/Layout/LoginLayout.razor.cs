using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Web.Client.Services;
using Web.Client.Shared.Theme;
using Web.Services;

namespace Web.Components.Layout;

public partial class LoginLayout : LayoutComponentBase, IDisposable
{
    [CascadingParameter]
    private Task<AuthenticationState>? authenticationState { get; set; }
    [Inject] private LayoutService LayoutService { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] NavigationManager navManager { get; set; }
    [Inject] private ISessionStorageService _sessionStorageService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    private MudThemeProvider _mudThemeProvider;
    [CascadingParameter] private HttpContext? HttpContext { get; set; }

    protected override void OnParametersSet()
    {
        if (HttpContext is null)
        {
            // If this code runs, we're currently rendering in interactive mode, so there is no HttpContext.
            // The identity pages need to set cookies, so they require an HttpContext. To achieve this we
            // must transition back from interactive mode to a server-rendered page.
            NavigationManager.Refresh(forceReload: true);
        }
    }
    protected override async Task  OnInitializedAsync()
    {
        LayoutService.SetBaseTheme(Themes.LandingPageTheme());
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
        LayoutService.MajorUpdateOccurred += LayoutServiceOnMajorUpdateOccured;
        base.OnInitialized();
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