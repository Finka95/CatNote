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

        //CreateMap<AchievementEntity, Achievement>()
        //    .ForMember(x => x,
        //        opt => opt.MapFrom<AchievementResolver>());

        CreateMap<AchievementEntity, Achievement>().ConvertUsing<AchievementResolver>();

        CreateMap<Achievement, AchievementEntity>().ConvertUsing<AchievementEntityResolver>();

        //    CreateMap<Achievement, AchievementEntity>()
        //        .ForMember(x => x,
        //            opt => opt.MapFrom<AchievementEntityResolver>());
    }
}

public class AchievementEntityResolver : ITypeConverter<Achievement, AchievementEntity>
{
    public AchievementEntity Resolve(Achievement source, AchievementEntity destination, AchievementEntity destMember,
        ResolutionContext context)
    {
        return new AchievementEntity
        {
            Id = source.AchievementId,
            Title = source.Title,
            Description = source.Description,
            AchievementType = source.AchievementType
        };
    }

    public AchievementEntity Convert(Achievement source, AchievementEntity destination, ResolutionContext context)
    {
        return new AchievementEntity
        {
            Id = source.AchievementId,
            Title = source.Title,
            Description = source.Description,
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
            case AchievementType.ToAddFirst:
                return new AchievementToAddFirstTask
                {
                    Title = source.Title,
                    Description = source.Description,
                    AchievementId = source.Id
                };
                break;
            case AchievementType.ToAddFirstThree:
                return new AchievementToAddFirstThreeTask
                {
                    Title = source.Title,
                    Description = source.Description,
                    AchievementId = source.Id
                };
                break;
            case AchievementType.ToAddFirstFive:
                return new AchievementToAddFirstFiveTask
                {
                    Title = source.Title,
                    Description = source.Description,
                    AchievementId = source.Id
                };
                break;
            case AchievementType.CompletedFirstTask:
                return new AchievementCompletedFirstTask
                {
                    Title = source.Title,
                    Description = source.Description,
                    AchievementId = source.Id
                };
                break;
            case AchievementType.CompletedFirstThreeTasks:
                return new AchievementCompletedFirstThreeTask
                {
                    Title = source.Title,
                    Description = source.Description,
                    AchievementId = source.Id
                };
            default: return null;
        }
    }

    //public Achievement Resolve(AchievementEntity source, Achievement destination, Achievement destMember,
    //    ResolutionContext context)
    //{
    //    switch (source.AchievementType)
    //    {
    //        case AchievementType.ToAddFirst:
    //            return new AchievementToAddFirstTask
    //            {
    //                Title = source.Title,
    //                Description = source.Description,
    //                AchievementId = source.Id
    //            };
    //            break;
    //        case AchievementType.ToAddFirstThree:
    //            return new AchievementToAddFirstThreeTask
    //            {
    //                Title = source.Title,
    //                Description = source.Description,
    //                AchievementId = source.Id
    //            };
    //            break;
    //        case AchievementType.ToAddFirstFive:
    //            return new AchievementToAddFirstFiveTask
    //            {
    //                Title = source.Title,
    //                Description = source.Description,
    //                AchievementId = source.Id
    //            };
    //            break;
    //        case AchievementType.CompletedFirstTask:
    //            return new AchievementCompletedFirstTask
    //            {
    //                Title = source.Title,
    //                Description = source.Description,
    //                AchievementId = source.Id
    //            };
    //            break;
    //        case AchievementType.CompletedFirstThreeTasks:
    //            return new AchievementCompletedFirstThreeTask
    //            {
    //                Title = source.Title,
    //                Description = source.Description,
    //                AchievementId = source.Id
    //            };
    //        default: return null;
    //    }
    //}
}
