﻿// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Blazored.LocalStorage;
using Web.Client.Shared.NotificationContent;

namespace Web.Client.Services.Notifications;

public class InMemoryNotificationService : INotificationService
{
    private const string LocalStorageKey = "__notficationTimestamp";
    private readonly ILocalStorageService _localStorageService;
    private readonly ILogger<InMemoryNotificationService> _logger;

    private readonly List<NotificationMessage> _messages;

    public InMemoryNotificationService(ILocalStorageService localStorageService,
        ILogger<InMemoryNotificationService> logger)
    {
        _localStorageService = localStorageService;
        _logger = logger;

        _messages = new List<NotificationMessage>();
    }

    public async Task<bool> AreNewNotificationsAvailable()
    {
        var timestamp = await GetLastReadTimestamp();
        var entriesFound = _messages.Any(x => x.PublishDate > timestamp);

        return entriesFound;
    }

    public async Task MarkNotificationsAsRead()
    {
        await _localStorageService.SetItemAsync(LocalStorageKey, DateTime.UtcNow.Date);
    }

    public async Task MarkNotificationsAsRead(string id)
    {
        var message = await GetMessageById(id);
        if (message == null) return;

        var timestamp = await _localStorageService.GetItemAsync<DateTime>(LocalStorageKey);
        if (message.PublishDate > timestamp)
            await _localStorageService.SetItemAsync(LocalStorageKey, message.PublishDate);
    }

    public Task<NotificationMessage> GetMessageById(string id)
    {
        return Task.FromResult(_messages.FirstOrDefault(x => x.Id == id));
    }

    public async Task<IDictionary<NotificationMessage, bool>> GetNotifications()
    {
        var lastReadTimestamp = await GetLastReadTimestamp();
        var items = _messages.ToDictionary(x => x, x => lastReadTimestamp > x.PublishDate);
        return items;
    }

    public Task AddNotification(NotificationMessage message)
    {
        _messages.Add(message);
        return Task.CompletedTask;
    }

    private async Task<DateTime> GetLastReadTimestamp()
    {
        if (await _localStorageService.ContainKeyAsync(LocalStorageKey) == false) return DateTime.MinValue;

        var timestamp = await _localStorageService.GetItemAsync<DateTime>(LocalStorageKey);
        return timestamp;
    }


    public void Preload()
    {
        _messages.Add(new NotificationMessage(
            typeof(Announcement_v7_In_Development).Name,
            "v7 Is Coming!",
            "Learn about the upcoming major version",
            "Announcement",
            new DateTime(2024, 04, 20),
            "https://github.com/MudBlazor/MudBlazor/blob/f979c2c84e3ddd5f01a20ebc1102838d32a4b01b/content/Nuget.png",
            [
                new NotificationAuthor("The MudBlazor Team",
                    "https://mudblazor.com/_content/MudBlazor.Docs/images/logo.png")
            ], typeof(Announcement_v7_In_Development)));
    }
}