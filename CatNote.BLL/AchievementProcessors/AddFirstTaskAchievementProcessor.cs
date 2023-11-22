//using CatNote.BLL.Interfaces;
//using CatNote.DAL.Interfaces;
//using CatNote.Domain.Enums;

//namespace CatNote.BLL.AchievementProcessors;

//internal class AddFirstTaskAchievementProcessor : IAchievementProcessor
//{
//    private readonly ITaskRepository _taskRepository;

//    public AddFirstTaskAchievementProcessor(ITaskRepository taskRepository)
//    {
//        _taskRepository = taskRepository;
//    }

//    public AchievementType AchievementType => AchievementType.ToAddFirst;

//    public async Task<bool> Execute(int userId, CancellationToken cancellationToken)
//    {
//        var userTasks = await _taskRepository.GetTasksByUserId(userId, cancellationToken);

//        if (userTasks.Count == 1)
//        {
//            return true;
//        }

//        return false;
//    }
//}
