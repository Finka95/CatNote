using CatNote.BLL.Interfaces;
using CatNote.DAL.Interfaces;
using CatNote.Domain.Enums;

namespace CatNote.BLL.AchievementProcessors;

public class AddFirstFiveTaskAchievementProcessor : IAchievementProcessor
{
    private readonly ITaskRepository _taskRepository;

    public AddFirstFiveTaskAchievementProcessor(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public AchievementType AchievementType => AchievementType.ToAddFirstFive;

    public async Task<bool> Execute(int userId, CancellationToken cancellationToken)
    {
        var userTasks = await _taskRepository.GetTasksByUserId(userId, cancellationToken);

        if (userTasks.Count == 5)
        {
            return true;
        }

        return false;
    }
}
