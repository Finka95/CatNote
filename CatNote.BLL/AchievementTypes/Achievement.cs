using CatNote.BLL.Models;
using CatNote.Domain.Enums;

namespace CatNote.BLL.AchievementTypes;
public class Achievement
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public AchievementType Type { get; set; }
    public int TaskCount { get; set; }
}
