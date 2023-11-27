using AutoMapper;
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
        CreateMap<AchievementEntity, AchievementModel>().ConvertUsing<AchievementResolver>();
        CreateMap<AchievementModel, AchievementEntity>().ConvertUsing<AchievementEntityResolver>();
    }
}

public class AchievementEntityResolver : ITypeConverter<AchievementModel, AchievementEntity>
{
    public AchievementEntity Convert(AchievementModel source, AchievementEntity destination, ResolutionContext context)
    {
        return new AchievementEntity
        {
            Id = source.Id,
            Title = source.Title,
            Description = source.Description,
            Point = source.Point,
            TaskCount = source.TaskCount,
            Type = source.Type
        };
    }
}

public class AchievementResolver : ITypeConverter<AchievementEntity, AchievementModel>
{
    public AchievementModel Convert(AchievementEntity source, AchievementModel destination, ResolutionContext context)
    {
        return new AchievementModel
        {
            Id = source.Id,
            Title = source.Title,
            Description = source.Description,
            Point = source.Point,
            TaskCount = source.TaskCount,
            Type = source.Type
        };
    }
}
