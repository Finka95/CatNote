using CatNote.API.DTO;
using CatNote.API.Mappers;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace CatNote.API.DI;

public static class DependencyMapper
{
    public static void AddMapperServices(this IServiceCollection services)
    {
        services.AddSingleton<IMapper<AchievementModel, AchievementDTO>, AchievementDTOMapper>();
        services.AddSingleton<IMapper<TaskModel, TaskDTO>, TaskDTOMapper>();
        services.AddSingleton<IMapper<UserModel, UserDTO>, UserDTOMapper>();
    }
}