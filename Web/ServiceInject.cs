using Web.Client.Feature.Register;
using Web.Client.Interfaces;
using Web.Services.ExamServices;
using Web.Services.ReferenceServices;
using Web.Services.ReportServices;
using Web.Services.RoomServices;
using Web.Services.UserExamServices;
using Web.Services.UserServices;

namespace Web;

public static class ServiceInject
{
    public static IServiceCollection InjectService(this IServiceCollection services)
    {
        services.AddScoped<IUser, UserService>();
        services.AddScoped<IExam, ExamService>();
        services.AddScoped<IReferences, ReferenceService>();
        services.AddScoped<IRoom, RoomService>();
        services.AddScoped<IUserExam, UserExamService>();
        services.AddScoped<IMailService, EmailService>();
        services.AddScoped<IReport, ReportService>();
        return services;
    }
}
