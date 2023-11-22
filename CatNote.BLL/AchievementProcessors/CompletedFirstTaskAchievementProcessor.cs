using CatNote.BLL.Interfaces;
using CatNote.DAL.Interfaces;
using CatNote.Domain.Enums;
using TaskStatus = CatNote.Domain.Enums.TaskStatus;

namespace CatNote.BLL.AchievementProcessors;

public class CompletedFirstTaskAchievementProcessor : IAchievementProcessor
{
    private readonly ITaskRepository _taskRepository;

    public CompletedFirstTaskAchievementProcessor(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public AchievementType AchievementType => AchievementType.CompletedFirstTask;

    public async Task<bool> Execute(int userId, CancellationToken cancellationToken)
    {
        var userTasks = await _taskRepository.GetTasksByUserId(userId, cancellationToken);
        var completeTasksCount = userTasks.Where(x => x.Status == TaskStatus.Done)?.Count();

        if (completeTasksCount == 1)
        {
            return true;
        }

        return false;
    }
}
