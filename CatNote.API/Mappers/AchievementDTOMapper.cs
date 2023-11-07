using CatNote.API.DTO;
using CatNote.BLL.Models;
using CatNote.Domain.Interfaces;

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