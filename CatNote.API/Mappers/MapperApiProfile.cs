using AutoMapper;
using CatNote.API.DTO;
using CatNote.BLL.AchievementTypes;
using CatNote.BLL.Models;

namespace CatNote.API.Mappers;

public class MapperApiProfile : Profile
{
    public MapperApiProfile()
    {
        CreateMap<UserDTO, UserModel>().ReverseMap();
        CreateMap<TaskDTO, TaskModel>().ReverseMap();
        CreateMap<AchievementDTO, Achievement>().ReverseMap();
    }
}
