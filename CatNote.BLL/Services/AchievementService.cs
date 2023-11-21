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
using CatNote.BLL.AchievementType;

namespace CatNote.BLL.Services;

public class AchievementService : GenericService<Achievement, AchievementEntity>, IAchievementService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<AchievementEntity> _genericRepository;
    private readonly IAchievementRepository _achievementRepository;

    public AchievementService(IMapper mapper, IGenericRepository<AchievementEntity> genericRepository, IAchievementRepository achievementRepository)
        :base(mapper, genericRepository)
    {
        _mapper = mapper;
        _genericRepository = genericRepository;
        _achievementRepository = achievementRepository;
    }

    public async Task CheckAchievement(int userId, CancellationToken cancellationToken)
    {
        var achievementsEntities = await _genericRepository.GetAll(cancellationToken);
        var userAchievementsEntities = await _achievementRepository.GetAchievementsByUserId(userId, cancellationToken);

        var exceptAchievementsEntities = achievementsEntities.Except(userAchievementsEntities).ToList();

        var achievements = _mapper.Map<List<Achievement>>(exceptAchievementsEntities); //TODO раскинуть в 2 вида ачивок с маппером (временно без маппера)

        //var achievementsToAdd = achievements.Where(x => x.AchievementType == Domain.Enums.AchievementType.ToAdd).Select(x => new )
        //var achievementsToExecute = achievements.Where(x => x.AchievementType == Domain.Enums.AchievementType.ToExecute).ToList();

        foreach (var achievement in achievements)
        {
            var result = await achievement.Execute(userId, cancellationToken); // проверка что условие для ачивки выполнено

            if (result)
            {
                await _achievementRepository.AddConnection(achievement.Title, userId, cancellationToken);
            }
        }

        //if (achievements.Where(x => x.Title == name) == null)
        //{
        //    await _achievementRepository.AddConnection(name, userId, cancellationToken);
        //}
    }
}
