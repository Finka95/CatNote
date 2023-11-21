using CatNote.BLL.AchievementTypes;

namespace CatNote.BLL.Models;

public class UserModel
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public IEnumerable<TaskModel>? Tasks { get; set; }
    public IEnumerable<Achievement>? Achievements { get; set;}
}
