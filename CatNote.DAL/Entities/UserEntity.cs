namespace CatNote.DAL.Entities;

public class UserEntity : BaseEntity
{
    public string? UserName { get; set; } = null!;
    public ICollection<TaskEntity>? Tasks { get; set; } = new List<TaskEntity>();
    public ICollection<AchievementEntity>? Achievements { get; set; } = new List<AchievementEntity>();
}
