namespace CatNote.DAL.Entities;

public class AchievementEntity : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IEnumerable<UserEntity>? Users { get; set; }
}
