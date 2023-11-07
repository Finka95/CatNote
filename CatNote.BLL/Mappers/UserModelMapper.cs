using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.BLL.Mappers;

public static class UserModelMapper
{
    public static UserEntity ToEntity(UserModel userModel) => new UserEntity
    {
        Id = userModel.Id,
        UserName = userModel.UserName,
        Tasks = userModel.Tasks
            .Select(x => TaskModelMapper.ToEntity(x))
            .ToList(),
        Achievements = userModel.Achievements
            .Select(x => AchievementModelMapper.ToEntity(x))
            .ToList()
    };

    public static UserModel FromEntity(UserEntity userEntity) => new UserModel
    {
        Id = userEntity.Id,
        UserName = userEntity.UserName,
        Tasks = userEntity.Tasks
            .Select(x => TaskModelMapper.FromEntity(x))
            .ToList(),
        Achievements = userEntity.Achievements
            .Select(x => AchievementModelMapper.FromEntity(x))
            .ToList()
    };
}