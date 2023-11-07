namespace CatNote.API.DTO;

public class UserDTO
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public IEnumerable<TaskDTO>? Tasks { get; set; }
    public IEnumerable<AchievementDTO>? Achievements { get; set; }
}
