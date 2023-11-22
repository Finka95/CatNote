using CatNote.Domain.Enums;

namespace CatNote.BLL.Interfaces;

public interface IAchievementProcessor
{
    AchievementType AchievementType { get; }
    Task<bool> Execute(int userId, CancellationToken cancellationToken);
}

