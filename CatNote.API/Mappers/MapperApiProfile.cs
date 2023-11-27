using AutoMapper;
using CatNote.API.DTO;
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
        CreateMap<AchievementDTO, AchievementModel>();
        CreateMap<AchievementModel, AchievementDTO>();
    }
}
