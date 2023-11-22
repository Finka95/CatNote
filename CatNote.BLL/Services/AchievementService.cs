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
using CatNote.BLL.AchievementTypes;

namespace CatNote.BLL.Services;

public class AchievementService : GenericService<Achievement, AchievementEntity>, IAchievementService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<AchievementEntity> _genericRepository;
    private readonly IAchievementRepository _achievementRepository;
    private readonly IEnumerable<IAchievementProcessor> _achievementProcessors;

    public AchievementService(
        IMapper mapper, 
        IGenericRepository<AchievementEntity> genericRepository, 
        IAchievementRepository achievementRepository,
        IEnumerable<IAchievementProcessor> achievementProcessors)
        :base(mapper, genericRepository)
    {
        _mapper = mapper;
        _genericRepository = genericRepository;
        _achievementRepository = achievementRepository;
        _achievementProcessors = achievementProcessors;
    }

    public async Task CheckAchievement(int userId, CancellationToken cancellationToken)
    {
        var achievementsEntities = await _genericRepository.GetAll(cancellationToken);
        var userAchievementsEntities = await _achievementRepository.GetAchievementsByUserId(userId, cancellationToken);

        //var exceptAchievementsEntities = achievementsEntities.ExceptBy(userAchievementsEntities, x => x.Id);

        var exceptAchievementsEntities = achievementsEntities.ExceptBy(userAchievementsEntities.Select(x => x.AchievementType), x => x.AchievementType);

        var achievements = _mapper.Map<List<Achievement>>(exceptAchievementsEntities); //TODO раскинуть в 2 вида ачивок с маппером (временно без маппера)

        foreach (var achievement in achievements)
        {
            foreach (var processor in _achievementProcessors)
            {
                if (achievement.AchievementType == processor.AchievementType)
                {
                    var result = await processor.Execute(userId, cancellationToken);

                    if (result)
                    {
                        await _achievementRepository.AddConnection(achievement.Title, userId, cancellationToken);
                    }
                }
            }
        }

        //if (achievements.Where(x => x.Title == name) == null)
        //{
        //    await _achievementRepository.AddConnection(name, userId, cancellationToken);
        //}
    }
}
