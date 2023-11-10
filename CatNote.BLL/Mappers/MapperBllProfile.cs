using AutoMapper;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.BLL.Mappers;

public class MapperBllProfile : Profile
{
    public MapperBllProfile()
    {
        CreateMap<UserEntity, UserModel>().ReverseMap();
        CreateMap<TaskEntity, TaskModel>().ReverseMap();
        CreateMap<AchievementEntity, AchievementModel>().ReverseMap();
    }
}
