using CatNote.DAL.Entities;

namespace CatNote.DAL.Interfaces;

public interface IAchievementRepository : IGenericRepository<>
{
    Task<List<AchievementEntity>> GetAchievementsByUserId(int userId, CancellationToken cancellationToken);
    Task AddConnection(string achievementName, int userId, CancellationToken cancellationToken);
}