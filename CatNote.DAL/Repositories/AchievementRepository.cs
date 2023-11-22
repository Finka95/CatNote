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

    public async Task<IEnumerable<AchievementEntity>?> GetAchievementsByUserId(int userId, CancellationToken cancellationToken)
    {
        var achievementEntity = await _applicationDbContext.Users.Include(x => x.Achievements)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        return achievementEntity?.Achievements?.ToList();
    }

    public async Task AddConnection(List<AchievementEntity> achievementEntities, int userId, CancellationToken cancellationToken)
    {
        var userEntity = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (userEntity != null)
        {
            foreach (var entity in achievementEntities)
            {
                userEntity.Achievements?.Add(entity);
                entity.Users?.Add(userEntity);
            }
        }
        

        _applicationDbContext?.SaveChangesAsync(cancellationToken);
    }
}
