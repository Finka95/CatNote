using CatNote.Domain.Enums;

public class IAchievementType
{
    public int AchievementId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AchievementType AchievementType { get; set; }
}
