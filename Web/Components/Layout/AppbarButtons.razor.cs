// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Web.Common.Enums;
using Web.Components.Features.Auth;
using Web.Services;
using Web.Services.Notifications;

namespace Web.Components.Layout;

public partial class AppbarButtons
{
    [Inject] private INotificationService NotificationService { get; set; }
    [Inject] private LayoutService LayoutService { get; set; }
    [Inject] NavigationManager navManager { get; set; }
    [Inject] AuthenticationStateProvider authStateProvicder { get; set; }
    private IDictionary<NotificationMessage, bool> _messages = null;
    private bool _newNotificationsAvailable = false;

    public string DarkLightModeButtonText => LayoutService.CurrentDarkLightMode switch
    {
        DarkLightMode.Dark => "System mode",
        DarkLightMode.Light => "Dark mode",
        _ => "Light mode"
    };

    public string DarkLightModeButtonIcon => LayoutService.CurrentDarkLightMode switch
    {
        DarkLightMode.Dark => Icons.Material.Rounded.AutoMode,
        DarkLightMode.Light => Icons.Material.Outlined.DarkMode,
        _ => Icons.Material.Filled.LightMode
    };

    private async Task MarkNotificationAsRead()
    {
        await NotificationService.MarkNotificationsAsRead();
        _newNotificationsAvailable = false;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _newNotificationsAvailable = await NotificationService.AreNewNotificationsAvailable();
            _messages = await NotificationService.GetNotifications();
            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task Logout()
    {
        var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvicder;
        await customAuthStateProvider.UpdateAuthenticationState(null);
        navManager.NavigateTo("/login");
    }
}
