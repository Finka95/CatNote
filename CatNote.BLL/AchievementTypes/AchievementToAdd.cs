using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Models;
using CatNote.Domain.Enums;

namespace CatNote.BLL.AchievementTypes;

public class AchievementToAdd : Achievement
{
    public AchievementType AchievementType => AchievementType.ToAdd;
    public override bool Execute(UserModel user)
    {
        var userTasks = user.Tasks?.ToList();

        if (userTasks?.Count == this.TaskCount)
        {
            return true;
        }

        return false;
    }
}
