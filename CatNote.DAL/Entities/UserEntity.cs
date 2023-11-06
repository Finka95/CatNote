using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CatNote.DAL.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public IEnumerable<TaskEntity>? Tasks { get; set; }
    public IEnumerable<AchievementEntity>? Achievements { get; set;}
}
