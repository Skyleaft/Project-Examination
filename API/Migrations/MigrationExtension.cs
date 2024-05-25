using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Migrations;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using IdentityDbContext context = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        context.Database.Migrate();
    }
}