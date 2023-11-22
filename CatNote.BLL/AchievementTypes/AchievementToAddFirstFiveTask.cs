using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Models;
using CatNote.Domain.Enums;

namespace CatNote.BLL.AchievementTypes;

public class AchievementToAddFirstFiveTask : Achievement
{
    public AchievementType AchievementType { get; } = AchievementType.ToAddFirstFive;

    public override bool Execute(UserModel user)
    {
        var userTasks = user.Tasks?.ToList();

        if (userTasks?.Count == 5)
        {
            return true;
        }

        return false;
    }
}
