using CatNote.DAL.Interfaces;
using CatNote.DAL.Repositories;

namespace CatNote.BLL.AchievementType;

public class AchievementToAddFirstTask : Achievement
{
    private readonly ITaskRepository _taskRepository;

    public AchievementToAddFirstTask(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public int TasksCount { get; set; }
    public override async Task<bool> Execute(int userId, CancellationToken cancellationToken)
    {
        var userTasks = await _taskRepository.GetTasksByUserId(userId, cancellationToken);

        if (userTasks.Count == 1)
        {
            return true;
        }

        return false;
    }
}
