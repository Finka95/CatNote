﻿using CatNote.BLL.Interfaces;
using CatNote.DAL.Interfaces;
using CatNote.Domain.Enums;

namespace CatNote.BLL.AchievementProcessors;

public class AddFirstThreeTaskAchievementProcessor : IAchievementProcessor
{
    private readonly ITaskRepository _taskRepository;

    public AddFirstThreeTaskAchievementProcessor(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public AchievementType AchievementType => AchievementType.ToAddFirstThree;

    public async Task<bool> Execute(int userId, CancellationToken cancellationToken)
    {
        var userTasks = await _taskRepository.GetTasksByUserId(userId, cancellationToken);

        if (userTasks.Count == 3)
        {
            return true;
        }

        return false;
    }
}
