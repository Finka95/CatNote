using AutoMapper;
using CatNote.BLL.AchievementTypes;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.Domain.Enums;

namespace CatNote.BLL.Mappers;

public class MapperBllProfile : Profile
{
    public MapperBllProfile()
    {
        CreateMap<UserEntity, UserModel>().ReverseMap();
        CreateMap<TaskEntity, TaskModel>().ReverseMap();
        CreateMap<AchievementEntity, Achievement>().ConvertUsing<AchievementResolver>();
        CreateMap<Achievement, AchievementEntity>().ConvertUsing<AchievementEntityResolver>();
    }
}

public class AchievementEntityResolver : ITypeConverter<Achievement, AchievementEntity>
{
    public AchievementEntity Convert(Achievement source, AchievementEntity destination, ResolutionContext context)
    {
        return new AchievementEntity
        {
            Id = source.AchievementId,
            Title = source.Title,
            Description = source.Description,
            TaskCount = source.TaskCount,
            AchievementType = source.AchievementType
        };
    }
}

public class AchievementResolver : ITypeConverter<AchievementEntity, Achievement>
{
    public Achievement Convert(AchievementEntity source, Achievement destination, ResolutionContext context)
    {
        switch (source.AchievementType)
        {
            case AchievementType.ToAdd:
                return new AchievementToAdd
                {
                    AchievementId = source.Id,
                    Title = source.Title,
                    Description = source.Description,
                    TaskCount = source.TaskCount,
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
