namespace CatNote.BLL.Models;

public class UserModel
{
    public IEnumerable<Task>? Tasks { get; set; }
    public IEnumerable<AchievementModel>? Achievements { get; set;}
}
