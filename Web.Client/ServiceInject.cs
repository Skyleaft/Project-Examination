﻿using Web.Client.Feature.BankSoal;
using Web.Client.Feature.References;
using Web.Client.Feature.Roms;
using Web.Client.Feature.UserManagements;
using Web.Client.Interfaces;

namespace Web.Client;

public static class ServiceInject
{
    public static IServiceCollection InjectService(this IServiceCollection services)
    {
        services.AddScoped<IUser, UserService>();
        services.AddScoped<IExam, ExamService>();
        services.AddScoped<IReferences,ReferenceService>();
        services.AddScoped<IRoom, RoomService>();
        return services;
    }
}
