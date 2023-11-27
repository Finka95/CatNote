using CatNote.Domain.Enums;

namespace CatNote.API.DTO;

public class AchievementDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Point { get; set; }
    public AchievementType Type { get; set; }
    public int TaskCount { get; set; }
    public IEnumerable<UserDTO>? Users { get; set; }
}
