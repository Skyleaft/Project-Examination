using Web.Components.Features.UserManagement;

namespace Web;

public static class ServiceInject
{
    public static IServiceCollection InjectService(this IServiceCollection services)
    {
        services.AddScoped<IUser, UserService>();
        return services;
    }
}
