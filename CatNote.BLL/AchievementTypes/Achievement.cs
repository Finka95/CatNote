using CatNote.Domain.Enums;

namespace CatNote.BLL.AchievementTypes;

public class Achievement
{
    public int AchievementId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AchievementType AchievementType { get; set; }

    //public abstract Task<bool> Execute(int userId, CancellationToken cancellationToken);
}