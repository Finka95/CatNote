namespace CatNote.BLL.AchievementType;
using AchievementType = CatNote.Domain.Enums.AchievementType;

public abstract class Achievement
{
    public int AchievementId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AchievementType AchievementType { get; set; }

    public abstract Task<bool> Execute(int userId, CancellationToken cancellationToken);
}