using CatNote.Domain.Enums;

namespace CatNote.API.DTO;

public class AchievementDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public AchievementType AchievementType { get; set; }
    public int TaskCount { get; set; }
}
