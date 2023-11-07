using CatNote.API.DTO;
using CatNote.BLL.Mappers.Abstractions;
using CatNote.BLL.Models;

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

    public UserModel ToEntity(UserDTO userDTO) => new ()
    {
            Id = userDTO.Id,
            UserName = userDTO.UserName,
            Tasks = userDTO.Tasks?
                .Select(x => _taskMapper.ToEntity(x))
                .ToList(),
            Achievements = userDTO.Achievements?
                .Select(x => _achivementMapper.ToEntity(x))
                .ToList()
    };

    public UserDTO FromEntity(UserModel userModel) => new ()
    {
        Id = userModel.Id,
        UserName = userModel.UserName,
        Tasks = userModel.Tasks?
            .Select(x => _taskMapper.FromEntity(x))
            .ToList(),
        Achievements = userModel.Achievements?
            .Select(x => _achivementMapper.FromEntity(x))
            .ToList()
    };
}