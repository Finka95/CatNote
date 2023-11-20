using CatNote.Domain.Enums;

public class Achievement
{
    public int AchievementId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AchievementType AchievementType { get; set; }
}
