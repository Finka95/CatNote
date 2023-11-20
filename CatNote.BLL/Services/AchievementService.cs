using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Interfaces;
using AutoMapper;

namespace CatNote.BLL.Services;

public class AchievementService : GenericService<AchievementModel, AchievementEntity>, IAchievementService
{
    private readonly IAchievementRepository _achievementRepository;

    public AchievementService(IMapper mapper, IGenericRepository<AchievementEntity> genericRepository, IAchievementRepository achievementRepository)
        :base(mapper, genericRepository)
    {
        _achievementRepository = achievementRepository;
    }

    //public AchievementService(IAchievementRepository achievementRepository)
    //{
    //    _achievementRepository = achievementRepository;
    //}

    public async Task CheckAchievement(string name, int userId, CancellationToken cancellationToken)
    {
        var achievements = await _achievementRepository.GetAchievementsByUserId(userId, cancellationToken);

        if (achievements.Where(x => x.Title == name) == null)
        {
            await _achievementRepository.AddConnection(name, userId, cancellationToken);
        }
    }
}
