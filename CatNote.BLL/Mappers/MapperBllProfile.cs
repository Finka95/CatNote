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
            Id = source.Id,
            Title = source.Title,
            Description = source.Description,
            TaskCount = source.TaskCount,
            Type = source.Type
        };
    }
}

public class AchievementResolver : ITypeConverter<AchievementEntity, Achievement>
{
    public Achievement Convert(AchievementEntity source, Achievement destination, ResolutionContext context)
    {
        return new Achievement
        {
            Id = source.Id,
            Title = source.Title,
            Description = source.Description,
            TaskCount = source.TaskCount,
            Type = source.Type
        };
    }
}
