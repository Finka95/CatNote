namespace CatNote.DAL.Entities;

public class UserEntity : BaseEntity
{
    public string? UserName { get; set; } = null!;
    public IEnumerable<TaskEntity>? Tasks { get; set; }
    public IEnumerable<AchievementEntity>? Achievements { get; set;}
}
