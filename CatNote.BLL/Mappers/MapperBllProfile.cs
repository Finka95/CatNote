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

        CreateMap<AchievementEntity, Achievement>()
            .ForMember(x => x,
                opt => opt.MapFrom<AchievementResolver>());

        CreateMap<Achievement, AchievementEntity>()
            .ForMember(x => x,
                opt => opt.MapFrom<AchievementEntityResolver>());
    }
}

public class AchievementEntityResolver : IValueResolver<Achievement, AchievementEntity, AchievementEntity>
{
    public AchievementEntity Resolve(Achievement source, AchievementEntity destination, AchievementEntity destMember,
        ResolutionContext context)
    {
        throw new NotImplementedException();
    }
}

public class AchievementResolver : IValueResolver<AchievementEntity, Achievement, Achievement>
{
    public Achievement Resolve(AchievementEntity source, Achievement destination, Achievement destMember,
        ResolutionContext context)
    {
        switch (source.AchievementType)
        {
            case 0:
                return new AchievementToAddFirstTask();
            case 1:
                return 
            default: return null;
        }
    }
}
