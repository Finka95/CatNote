using CatNote.API.DTO;
using CatNote.BLL.Models;
using CatNote.Common.Interfaces;
using CatNote.DAL.Entities;

namespace CatNote.API.Mappers;

public class UserDTOMapper : IMapper<UserModel, UserDTO>
{
    private readonly IMapper<TaskModel, TaskDTO> _taskMapper;
    private readonly IMapper<AchievementModel, AchievementDTO> _achivementMapper;

    public UserDTOMapper(IMapper<TaskModel, TaskDTO> taskMapper, IMapper<AchievementModel, AchievementDTO> achivementMapper)
    {
        _taskMapper = taskMapper;
        _achivementMapper = achivementMapper;
    }

    public UserModel ToEntity(UserDTO userDTO) => new UserModel
    {
            Id = userDTO.Id,
            UserName = userDTO.UserName,
            Tasks = userDTO.Tasks
                .Select(x => _taskMapper.ToEntity(x))
                .ToList(),
            Achievements = userDTO.Achievements
                .Select(x => _achivementMapper.ToEntity(x))
                .ToList()
    };

    public UserDTO FromEntity(UserModel userModel) => new UserDTO
    {
        Id = userModel.Id,
        UserName = userModel.UserName,
        Tasks = userModel.Tasks.Select(x => _taskMapper.FromEntity(x)).ToList(),
        Achievements = userModel.Achievements.Select(x => _achivementMapper.FromEntity(x))
    };
}