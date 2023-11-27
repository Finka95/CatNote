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
        var userEntity = await _applicationDbContext.Users.Include(x => x.Achievements).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        var achievementEntities = await dbSet.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == achievementId, cancellationToken);

        if (userEntity != null && achievementEntities != null)
        {
            userEntity.Achievements!.Add(achievementEntities);
        }

        await _applicationDbContext!.SaveChangesAsync(cancellationToken);
    }

    public async Task<AchievementEntity?> GetAchievementByTaskCountAchievementType(int taskCount, AchievementType achievementType,
        CancellationToken cancellationToken)
    {
        var achievement = await dbSet.FirstOrDefaultAsync(x => x.Type == achievementType && x.TaskCount == taskCount, cancellationToken);

        return achievement;
    }

    public async Task<List<AchievementEntity>> GetAchievementsByUser(UserEntity userEntity, CancellationToken cancellationToken)
    {
        var result = await dbSet.Include(x => x.Users).Where(a => a.Users!.Contains(userEntity)).ToListAsync(cancellationToken);

        return result;
    }
}
