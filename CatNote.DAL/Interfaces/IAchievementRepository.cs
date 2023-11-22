using CatNote.DAL.Entities;

namespace CatNote.DAL.Interfaces;

public interface IAchievementRepository
{
    Task<IEnumerable<AchievementEntity>?> GetAchievementsByUserId(int userId, CancellationToken cancellationToken);
    Task AddConnection(List<AchievementEntity> achievements, int userId, CancellationToken cancellationToken);
}
