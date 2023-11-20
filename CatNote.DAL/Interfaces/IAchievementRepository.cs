using CatNote.DAL.Entities;

namespace CatNote.DAL.Interfaces;

public interface IAchievementRepository
{
    Task<List<AchievementEntity>> GetAchievementsByUserId(int userId, CancellationToken cancellationToken);
    Task AddConnection(string achievementName, int userId, CancellationToken cancellationToken);
}