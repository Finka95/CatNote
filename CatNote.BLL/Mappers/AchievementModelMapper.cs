using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.BLL.Mappers;

public static class AchievementModelMapper
{
    public static AchievementEntity ToEntity(AchievementModel achievementModel) => new AchievementEntity
    {
        Id = achievementModel.Id,
        Title = achievementModel.Title,
        Description = achievementModel.Description
    };

    public static AchievementModel FromEntity(AchievementEntity achievementEntity) => new AchievementModel
    {
        Id = achievementEntity.Id,
        Title = achievementEntity.Title,
        Description = achievementEntity.Description
    };
}