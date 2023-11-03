namespace CatNote.API.DTO;

public class UserDTO
{
    public string UserName { get; set; } = null!;
    public IEnumerable<TaskDTO>? Tasks { get; set; }
    public IEnumerable<AchievementDTO>? Achievements { get; set; }
}
