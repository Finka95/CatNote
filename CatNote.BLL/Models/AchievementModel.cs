using CatNote.Domain.Enums;

namespace CatNote.BLL.Models;
public class AchievementModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Point { get; set; }
    public AchievementType Type { get; set; }
    public int TaskCount { get; set; }
    public IEnumerable<UserModel>? Users { get; set; }
}
