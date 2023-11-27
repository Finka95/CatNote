namespace CatNote.DAL.Entities;

public class UserEntity : BaseEntity
{
    public string? UserName { get; set; } = null!;
    public ICollection<TaskEntity>? Tasks { get; set; }
    public ICollection<AchievementEntity>? Achievements { get; set; }
}
