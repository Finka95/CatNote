//using CatNote.BLL.Interfaces;
//using CatNote.BLL.Models;
//using CatNote.DAL.Entities;
//using CatNote.DAL.Interfaces;
//using CatNote.Domain.Enums;

//namespace CatNote.BLL.AchievementProcessors;

//public class AddFirstFiveTaskAchievementProcessor : IAchievementProcessor
//{

//    public AddFirstFiveTaskAchievementProcessor()
//    {
//    }

//    public AchievementType AchievementType => AchievementType.ToAddFirstFive;

//    public async Task<bool> Execute(UserModel user, CancellationToken cancellationToken)
//    {
//        var userTasks = await _taskRepository.GetTasksByUserId(userId, cancellationToken);

//        user.Tasks

//        if (userTasks.Count == 5)
//        {
//            return true;
//        }

//        return false;
//    }
//}
