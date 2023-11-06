namespace CatNote.BLL.Models;

public class UserModel
{
    public IEnumerable<TaskModel>? Tasks { get; set; }
    public IEnumerable<AchievementModel>? Achievements { get; set;}
}
