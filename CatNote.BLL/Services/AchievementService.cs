using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatNote.BLL.Services;

public class AchievementService
{
    private readonly IAchievementRepository _achievementRepository;

    public AchievementService(IAchievementRepository achievementRepository)
    {
        _achievementRepository = achievementRepository;
    }

    public async Task CheckAchievement(string name, int userId, CancellationToken cancellationToken)
    {
        var achievements = await _achievementRepository.GetAchievementsByUserId(userId, cancellationToken);

        if (achievements.Where(x => x.Title == name) == null)
        {
            
        }
    }
}
