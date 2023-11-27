using CatNote.Domain.Enums;

namespace CatNote.DAL.Entities;

public class AchievementEntity : BaseEntity
{
    public string? Title { get; set; } 
    public string? Description { get; set; }
    public int Point { get; set; }
    public AchievementType Type { get; set; }
    public int TaskCount { get; set; }
    public ICollection<UserEntity>? Users { get; set; }
}
