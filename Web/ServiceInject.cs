using Web.Components.Features.UserManagement;
using Web.Services.ExamService;

namespace Web;

public static class ServiceInject
{
    public static IServiceCollection InjectService(this IServiceCollection services)
    {
        services.AddScoped<IUser, UserService>();
        services.AddScoped<IExam, ExamService>();
        return services;
    }
}
