using CatNote.BLL.Models;
using CatNote.Domain.Enums;

public abstract class Achievement
{
    public int AchievementId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AchievementType AchievementType { get; set; }

    public abstract bool Execute(UserModel user);
}
