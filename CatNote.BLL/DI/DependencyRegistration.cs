﻿using CatNote.BLL.AchievementType;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using CatNote.BLL.Services;
using CatNote.DAL.DI;
using CatNote.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CatNote.BLL.DI;
public static class DependencyRegistration
{
    public static void AddBusinessServices(this IServiceCollection services, string connectionString)
    {
        services.AddDatabaseServices(connectionString);

        services.AddScoped<IGenericService<TaskModel>, TaskService>();
        services.AddScoped<IGenericService<Achievement>, GenericService<Achievement, AchievementEntity>>();
        services.AddScoped<IGenericService<UserModel>, GenericService<UserModel, UserEntity>>();

        //services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IAchievementService, AchievementService>();
    }
}
