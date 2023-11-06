using Microsoft.AspNetCore.Identity;

namespace CatNote.DAL.Entities;

public class UserEntity
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public IEnumerable<TaskEntity>? Tasks { get; set; }
    public IEnumerable<AchievementEntity>? Achievements { get; set;}
}
