using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using Web.Common.Extensions;
using Web.Components;
using Web.Components.Features.Auth;
using Web.Services;
using Web.Services.Notifications;
using Web.Services.UserPreferences;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddMudServices();
builder.Services.AddSystemd();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.Cookie.Name = "auth_token";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IUserPreferencesService, UserPreferencesService>();
builder.Services.AddScoped<INotificationService, InMemoryNotificationService>();
builder.Services.AddScoped<LayoutService>();
builder.Services.AddScoped<Navigation>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiURL = builder.Configuration.GetConnectionString("DefaultConnection") ?? "https+http://apiservice";
builder.Services.AddHttpClient("API", (sp, cl) => { cl.BaseAddress = new Uri(apiURL); });
builder.Services.AddScoped(
    sp => sp.GetService<IHttpClientFactory>().CreateClient("API"));

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
app.UseAuthentication();
app.UseAuthorization();

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