using CatNote.API.DTO;
using CatNote.BLL.Models;
using CatNote.Common.Interfaces;
using CatNote.DAL.Entities;

namespace CatNote.API.Mappers;

public class AchievementDTOMapper : IMapper<AchievementModel, AchievementDTO>
{
    public AchievementModel ToEntity(AchievementDTO achievementDTO) => new AchievementModel
    {
        Id = achievementDTO.Id,
        Title = achievementDTO.Title,
        Description = achievementDTO.Description
    };

    public AchievementDTO FromEntity(AchievementModel achievementModel) => new AchievementDTO
    {
        Id = achievementModel.Id,
        Title = achievementModel.Title,
        Description = achievementModel.Description
    };
}