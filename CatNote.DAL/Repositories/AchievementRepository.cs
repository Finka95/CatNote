using CatNote.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CatNote.DAL.Entities;

namespace CatNote.DAL.Repositories;

public class AchievementRepository : IAchievementRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AchievementRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<AchievementEntity>> GetAchievementsByUserId(int userId, CancellationToken cancellationToken)
    {
        var achievementEntity = await _applicationDbContext.Users.Include(x => x.Achievements)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        return achievementEntity.Achievements.ToList();
    }

    public async Task AddConnection(string achievementName, int userId, CancellationToken cancellationToken)
    {
        var achievementEntity = await _applicationDbContext.Achievements
            .Include(achievementEntity => achievementEntity.Users).FirstOrDefaultAsync(x => x.Title == achievementName, cancellationToken);
        var userEntity = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        achievementEntity?.Users?.Add(userEntity);
        userEntity?.Achievements?.Add(achievementEntity);

        _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}
