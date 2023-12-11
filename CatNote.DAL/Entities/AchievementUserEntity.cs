namespace CatNote.DAL.Entities;

public class AchievementUserEntity
{
    public int UserId { get; set; }
    public int AchievementId { get; set;}
    public UserEntity User { get; set; } = null!;
    public AchievementEntity Achievement { get; set; } = null!;
}
