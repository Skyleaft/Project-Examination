using Blazored.LocalStorage;
using MudBlazor.Services;
using Web.Common.Extensions;
using Web.Components;
using Web.Services;
using Web.Services.Notifications;
using Web.Services.UserPreferences;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddMudServices();
builder.Services.AddSystemd();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IUserPreferencesService, UserPreferencesService>();
builder.Services.AddScoped<INotificationService, InMemoryNotificationService>();
builder.Services.AddScoped<LayoutService>();
builder.Services.AddScoped<Navigation>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

using (var scope = app.Services.CreateScope())
{
    var notificationService = scope.ServiceProvider.GetService<INotificationService>();
    if (notificationService is InMemoryNotificationService inMemoryService)
    {
        inMemoryService.Preload();
    }
}

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();