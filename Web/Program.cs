using System.Text.Json.Serialization;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using FastEndpoints;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MudExtensions.Services;
using Shared.Common;
using Shared.Users;
using Web;
using Web.Client.Feature.BankSoal.Page;
using Web.Client.Services;
using Web.Client.Services.Notifications;
using Web.Client.Services.UserPreferences;
using Web.Client.Shared.Extensions;
using Web.Client.Shared.Models;
using Web.Common.Database;
using Web.Components;
using Web.Components.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddMudServices();
builder.Services.AddMudExtensions();
builder.Services.AddSystemd();
builder.Services.InjectService();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingAuthenticationStateProvider>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

var connectionString = builder.Configuration.GetConnectionString("mzserver")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(connectionString), optionsLifetime: ServiceLifetime.Transient);

builder.Services.AddIdentityCore<ApplicationUser>(o =>
        {
            o.SignIn.RequireConfirmedAccount = true;
            o.Password.RequireDigit = false;
            o.Password.RequireUppercase = false;
            o.Password.RequiredUniqueChars = 0;
            o.Password.RequireNonAlphanumeric = false;
        }
    )
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserPreferencesService, UserPreferencesService>();
builder.Services.AddScoped<INotificationService, InMemoryNotificationService>();
builder.Services.AddScoped<LayoutService>();
builder.Services.AddScoped<Navigation>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddFastEndpoints();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.MapDefaultEndpoints();
app.UseFastEndpoints(c => { c.Endpoints.RoutePrefix = "api"; });

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}
else
    app.UseWebAssemblyDebugging();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

using (var scope = app.Services.CreateScope())
{
    var notificationService = scope.ServiceProvider.GetService<INotificationService>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Superuser", "Operator", "Dosen", "User" };
    if (roleManager.Roles.Count() == 0)
        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));


    var superuser = new ApplicationUser
    {
        UserName = "sysadmin",
        NormalizedUserName = "SYSADMIN",
        NamaLengkap = "System Administrator",
        Gender = Gender.LakiLaki,
        Email = "milzan_malik@outlook.com",
        NormalizedEmail = "MILZAN_MALIK@OUTLOOK.COM",
        EmailConfirmed = true,
        KotaId = "1",
        SecurityStamp = Guid.NewGuid().ToString("D")
    };

    if (await userManager.FindByNameAsync(superuser.UserName) == null)
    {
        var created = await userManager.CreateAsync(superuser, "@superuser");
        if (created.Succeeded) await userManager.AddToRoleAsync(superuser, "Superuser");
        //await userManager.AddToRoleAsync(superuser, "Superuser");
    }


    if (notificationService is InMemoryNotificationService inMemoryService) inMemoryService.Preload();
}

app.UseStatusCodePagesWithRedirects("/StatusCode/{0}");

app.MapRazorComponents<App>()
    .DisableAntiforgery()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CreateSoalPage).Assembly);

app.MapAdditionalIdentityEndpoints();

app.Run();