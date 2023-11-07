using CatNote.BLL.Mappers.Abstractions;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.BLL.Mappers;

public class AchievementModelMapper : IMapper<AchievementEntity, AchievementModel>
{
    public AchievementEntity ToEntity(AchievementModel achievementModel) => new ()
    {
        Id = achievementModel.Id,
        Title = achievementModel.Title,
        Description = achievementModel.Description
    };

    public AchievementModel FromEntity(AchievementEntity achievementEntity) => new ()
    {
        Id = achievementEntity.Id,
        Title = achievementEntity.Title,
        Description = achievementEntity.Description
    };
}