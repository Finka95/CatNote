using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using CatNote.DAL.Interfaces;
using CatNote.Domain.Enums;
using TaskStatus = CatNote.Domain.Enums.TaskStatus;

namespace CatNote.BLL.AchievementTypes;

public class AchievementCompletedFirstTask : Achievement
{
    public AchievementType AchievementType => AchievementType.CompletedFirstTask;

    public override bool Execute(UserModel user)
    {
        var completeTasksCount = user.Tasks?.Where(x => x.Status == TaskStatus.Done)?.Count();

        if (completeTasksCount == 1)
        {
            return true;
        }

        return false;
    }
}
