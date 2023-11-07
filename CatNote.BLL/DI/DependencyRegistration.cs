using CatNote.DAL.Entities;
using CatNote.DAL.Repositories.Interfaces;
using CatNote.DAL.Repositories;
using CatNote.DAL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Mappers;
using CatNote.BLL.Mappers.Abstractions;
using CatNote.DAL.DI;
using CatNote.BLL.Models;
using CatNote.BLL.Services;

namespace CatNote.BLL.DI;
public static class DependencyRegistration
{
    public static void AddBusinessServices(this IServiceCollection services, string connectionString)
    {
        services.AddDatabaseServices(connectionString);

        services.AddSingleton<IMapper<AchievementEntity, AchievementModel>, AchievementModelMapper>();
        services.AddSingleton<IMapper<TaskEntity, TaskModel>, TaskModelMapper>();
        services.AddSingleton<IMapper<UserEntity, UserModel>, UserModelMapper>();

        services.AddScoped<IGenericService<TaskModel>, GenericService<TaskModel, TaskEntity>>();
        services.AddScoped<IGenericService<AchievementModel>, GenericService<AchievementModel, AchievementEntity>>();
        services.AddScoped<IGenericService<UserModel>, GenericService<UserModel, UserEntity>>();

    }
}
