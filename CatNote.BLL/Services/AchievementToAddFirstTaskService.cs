using CatNote.DAL.Interfaces;

namespace CatNote.BLL.Services;

public class AchievementToAddFirstTaskService
{
    private readonly ITaskRepository _taskRepository;

    public AchievementToAddFirstTaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<bool> Execute(int userId, CancellationToken cancellationToken)
    {
        var userTasks = await _taskRepository.GetTasksByUserId(userId, cancellationToken);

        if (userTasks.Count == 1)
        {
            return true;
        }

        return false;
    }
}
