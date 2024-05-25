using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Web.Common.Theme;
using Web.Services;

namespace Web.Components.Layout;

public partial class LoginLayout : LayoutComponentBase, IDisposable
{
    [CascadingParameter]
    private Task<AuthenticationState>? authenticationState { get; set; }
    [Inject] private LayoutService LayoutService { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] NavigationManager navManager { get; set; }

    private MudThemeProvider _mudThemeProvider;
    protected override async Task  OnInitializedAsync()
    {
        if (authenticationState is not null)
        {
            var authState = await authenticationState;
            var user = authState?.User;

            if (user?.Identity is not null && user.Identity.IsAuthenticated)
            {
                navManager.NavigateTo("/");
            }
        }
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