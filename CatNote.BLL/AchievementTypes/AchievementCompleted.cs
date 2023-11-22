using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Models;
using CatNote.Domain.Enums;
using TaskStatus = CatNote.Domain.Enums.TaskStatus;

namespace CatNote.BLL.AchievementTypes;

public class AchievementCompleted : Achievement
{
    public AchievementType AchievementType => AchievementType.CompletedTask;

    public override bool Execute(UserModel user)
    {
        var completeTasksCount = user.Tasks?.Where(x => x.Status == TaskStatus.Done)?.Count();

        if (completeTasksCount == TaskCount)
        {
            return true;
        }

        return false;
    }
}
