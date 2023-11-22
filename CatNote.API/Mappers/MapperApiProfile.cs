using AutoMapper;
using CatNote.API.DTO;
using CatNote.BLL.AchievementTypes;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.Domain.Enums;

namespace CatNote.API.Mappers;

public class MapperApiProfile : Profile
{
    public MapperApiProfile()
    {
        CreateMap<UserDTO, UserModel>().ReverseMap();
        CreateMap<TaskDTO, TaskModel>().ReverseMap();
        CreateMap<AchievementDTO, Achievement>().ConvertUsing<AchievementResolver>();
        CreateMap<Achievement, AchievementDTO>().ConvertUsing<AchievementEntityResolver>();
    }
}

public class AchievementEntityResolver : ITypeConverter<Achievement, AchievementDTO>
{
    public AchievementDTO Convert(Achievement source, AchievementDTO destination, ResolutionContext context)
    {
        return new AchievementDTO
        {
            Id = source.AchievementId,
            Title = source.Title,
            Description = source.Description,
            AchievementType = source.AchievementType,
            TaskCount = source.TaskCount
        };
    }
}

public class AchievementResolver : ITypeConverter<AchievementDTO, Achievement>
{
    public Achievement Convert(AchievementDTO source, Achievement destination, ResolutionContext context)
    {
        switch (source.AchievementType)
        {
            case AchievementType.ToAdd:
                return new AchievementToAdd
                {
                    AchievementId = source.Id,
                    Title = source.Title,
                    Description = source.Description,
                    TaskCount = source.TaskCount
                };
            case AchievementType.CompletedTask:
                return new AchievementCompleted
                {
                    AchievementId = source.Id,
                    Title = source.Title,
                    Description = source.Description,
                    TaskCount = source.TaskCount
                };
            default: throw new ArgumentOutOfRangeException();
        }
    }
}
