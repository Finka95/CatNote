using CatNote.DAL.Entities;
using CatNote.Domain.Enums;

namespace CatNote.DAL.Interfaces;

public interface IAchievementRepository : IGenericRepository<AchievementEntity>
{
    Task AddConnectionBetweenUserAndAchievement(int achievementId, int userId, CancellationToken cancellationToken);
    Task<AchievementEntity?> GetAchievementByTaskCountAchievementType(int taskCount, AchievementType achievementType,
        CancellationToken cancellationToken);

    Task<List<AchievementEntity>> GetAchievementsByUser(UserEntity user, CancellationToken cancellationToken);
}
