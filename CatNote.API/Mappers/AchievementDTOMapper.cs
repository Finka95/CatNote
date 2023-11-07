using CatNote.API.DTO;
using CatNote.BLL.Mappers.Abstractions;
using CatNote.BLL.Models;

namespace CatNote.API.Mappers;

public class AchievementDTOMapper : IMapper<AchievementModel, AchievementDTO>
{
    public AchievementModel ToEntity(AchievementDTO achievementDTO) => new ()
    {
        Id = achievementDTO.Id,
        Title = achievementDTO.Title,
        Description = achievementDTO.Description
    };

    public AchievementDTO FromEntity(AchievementModel achievementModel) => new ()
    {
        Id = achievementModel.Id,
        Title = achievementModel.Title,
        Description = achievementModel.Description
    };
}