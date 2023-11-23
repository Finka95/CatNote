using CatNote.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CatNote.DAL.Entities;
using CatNote.Domain.Enums;

namespace CatNote.DAL.Repositories;

public class AchievementRepository : GenericRepository<AchievementEntity>, IAchievementRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AchievementRepository(ApplicationDbContext applicationDbContext)
    : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task AddConnectionBetweenUserAndAchievement(int achievementId, int userId, CancellationToken cancellationToken)
    {
        var userTask = _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        var achievementTask = _applicationDbContext.Achievements.FirstOrDefaultAsync(x => x.Id == achievementId, cancellationToken);

        await Task.WhenAll(userTask, achievementTask);

        var userEntity = await userTask;
        var achievementEntities = await achievementTask;

        if (userEntity != null && achievementEntities != null)
        {
            userEntity.Achievements?.Add(achievementEntities);
            achievementEntities.Users?.Add(userEntity);
        }
        

        _applicationDbContext?.SaveChangesAsync(cancellationToken);
    }

    public async Task<AchievementEntity?> GetAchievementByTaskCountAchievementType(int taskCount, AchievementType achievementType,
        CancellationToken cancellationToken)
    {
        var achievement = await _applicationDbContext.Achievements.FirstOrDefaultAsync(x => x.AchievementType == achievementType && x.TaskCount == taskCount, cancellationToken);

        return achievement;
    }
}
