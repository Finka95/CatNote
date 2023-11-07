using CatNote.BLL.Mappers.Abstractions;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.BLL.Mappers;

public class UserModelMapper : IMapper<UserEntity, UserModel>
{
    private readonly IMapper<TaskEntity, TaskModel> _taskMapper;
    private readonly IMapper<AchievementEntity, AchievementModel> _achievementsMapper;

    public UserModelMapper(IMapper<TaskEntity, TaskModel> taskMapper, IMapper<AchievementEntity, AchievementModel> achievementsMapper)
    {
        _taskMapper = taskMapper;
        _achievementsMapper = achievementsMapper;
    }

    public UserEntity ToEntity(UserModel userModel) => new ()
    {
        Id = userModel.Id,
        UserName = userModel.UserName,
        Tasks = userModel.Tasks?
            .Select(x => _taskMapper.ToEntity(x))
            .ToList(),
        Achievements = userModel.Achievements?
            .Select(x => _achievementsMapper.ToEntity(x))
            .ToList()
    };

    public UserModel FromEntity(UserEntity userEntity) => new ()
    {
        Id = userEntity.Id,
        UserName = userEntity.UserName,
        Tasks = userEntity.Tasks?
            .Select(x => _taskMapper.FromEntity(x))
            .ToList(),
        Achievements = userEntity.Achievements?
            .Select(x => _achievementsMapper.FromEntity(x))
            .ToList()
    };
}