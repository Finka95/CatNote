using CatNote.Domain.Enums;

namespace CatNote.BLL.AchievementProcessors;

public interface IAchievementProcessor
{
    AchievementType AchievementType { get; }
    Task<bool> Execute(int userId, CancellationToken cancellationToken);
}

