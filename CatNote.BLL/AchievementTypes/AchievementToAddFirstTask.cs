using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using CatNote.DAL.Interfaces;
using CatNote.DAL.Repositories;
using CatNote.Domain.Enums;

namespace CatNote.BLL.AchievementTypes;

public class AchievementToAddFirstTask : Achievement
{
    public AchievementType AchievementType { get; } = AchievementType.ToAddFirst;

    public override bool Execute(UserModel user)
    {
        var userTasks = user.Tasks?.ToList();

        if (userTasks?.Count == 1)
        {
            return true;
        }

        return false;
    }
}
