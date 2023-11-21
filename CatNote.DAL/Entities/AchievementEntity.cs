using CatNote.Domain.Enums;

namespace CatNote.DAL.Entities;

public class AchievementEntity : BaseEntity
{
    public string? Title { get; set; } 
    public string? Description { get; set; }
    public int AchievementTypeNum { get; set; } //переделать не в число
    public ICollection<UserEntity>? Users { get; set; }
}
