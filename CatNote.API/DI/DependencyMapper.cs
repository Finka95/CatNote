using CatNote.API.DTO;
using CatNote.API.Mappers;
using CatNote.BLL.Models;
using CatNote.Common.Interfaces;
using CatNote.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
namespace CatNote.API.DI;

public static class DependencyMapper
{
    public static void AddMapperServices(this IServiceCollection services)
    {
        services.AddSingleton<IMapper<UserModel, UserDTO>, UserDTOMapper>();
        services.AddSingleton<IMapper<AchievementModel, AchievementDTO>, AchievementDTOMapper>();
        services.AddSingleton<IMapper<TaskModel, TaskDTO>, TaskDTOMapper>();
    }
}