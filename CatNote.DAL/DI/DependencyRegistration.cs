using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using CatNote.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatNote.DAL.DI;

public static class DependencyRegistration
{
    public static void AddDatabaseServices(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IGenericRepository<UserEntity>, GenericRepository<UserEntity>>();
        services.AddScoped<IGenericRepository<AchievementEntity>, GenericRepository<AchievementEntity>>();
        services.AddScoped<IGenericRepository<TaskEntity>, GenericRepository<TaskEntity>>();

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
    }
}
