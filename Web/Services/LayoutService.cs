﻿// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MudBlazor;
using Web.Common.Enums;
using Web.Common.Models;
using Web.Services.UserPreferences;

namespace Web.Services;


public class LayoutService
{
    private readonly IUserPreferencesService _userPreferencesService;
    private UserPreferences.UserPreferences _userPreferences;
    private bool _systemPreferences;

    public bool IsRTL { get; private set; } = false;
    public DarkLightMode DarkModeToggle = DarkLightMode.System;

    public bool IsDarkMode { get; private set; }

    public MudTheme CurrentTheme { get; private set; }


    public LayoutService(IUserPreferencesService userPreferencesService)
    {
        _userPreferencesService = userPreferencesService;
    }

    public void SetDarkMode(bool value)
    {
        IsDarkMode = value;
    }

    public async Task ApplyUserPreferences(bool isDarkModeDefaultTheme)
    {
        _systemPreferences = isDarkModeDefaultTheme;
        _userPreferences = await _userPreferencesService.LoadUserPreferences();
        if (_userPreferences != null)
        {
            IsDarkMode = _userPreferences.DarkLightTheme switch
            {
                DarkLightMode.Dark => true,
                DarkLightMode.Light => false,
                DarkLightMode.System => isDarkModeDefaultTheme,
                _ => IsDarkMode
            };
            IsRTL = _userPreferences.RightToLeft;
        }
        else
        {
            IsDarkMode = isDarkModeDefaultTheme;
            _userPreferences = new UserPreferences.UserPreferences {DarkLightTheme = DarkLightMode.System};
            await _userPreferencesService.SaveUserPreferences(_userPreferences);
        }
    }

    public async Task OnSystemPreferenceChanged(bool newValue)
    {
        _systemPreferences = newValue;
        if (DarkModeToggle == DarkLightMode.System)
        {
            IsDarkMode = newValue;
            OnMajorUpdateOccured();
        }
    }

    public event EventHandler MajorUpdateOccured;

    private void OnMajorUpdateOccured() => MajorUpdateOccured?.Invoke(this, EventArgs.Empty);

    public async Task ToggleDarkMode()
    {
        switch (DarkModeToggle)
        {
            case DarkLightMode.System:
                DarkModeToggle = DarkLightMode.Light;
                IsDarkMode = false;
                break;
            case DarkLightMode.Light:
                DarkModeToggle = DarkLightMode.Dark;
                IsDarkMode = true;
                break;
            case DarkLightMode.Dark:
                DarkModeToggle = DarkLightMode.System;
                IsDarkMode = _systemPreferences;
                break;
        }

        _userPreferences.DarkLightTheme = DarkModeToggle;
        await _userPreferencesService.SaveUserPreferences(_userPreferences);
        OnMajorUpdateOccured();
    }

    public async Task ToggleRightToLeft()
    {
        IsRTL = !IsRTL;
        _userPreferences.RightToLeft = IsRTL;
        await _userPreferencesService.SaveUserPreferences(_userPreferences);
        OnMajorUpdateOccured();
    }

    public void SetBaseTheme(MudTheme theme)
    {
        CurrentTheme = theme;
        OnMajorUpdateOccured();
    }

    public DocsBasePage GetDocsBasePage(string uri)
    {
        if (uri=="/")
        {
            return DocsBasePage.Home;
        }
        else if (uri=="/browse")
        {
            return DocsBasePage.BrowseGalery;
        }
        else if (uri=="/discover")
        {
            return DocsBasePage.DiscoverMore;
        }
        else
        {
            return DocsBasePage.None;
        }
    }
}
