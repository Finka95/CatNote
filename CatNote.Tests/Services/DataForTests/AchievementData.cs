using CatNote.BLL.AchievementTypes;
using CatNote.DAL.Entities;
using CatNote.Domain.Enums;

namespace CatNote.Tests.Services.DataForTests;

internal static class AchievementData
{
    internal static Achievement AchievementModel =
        new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription", TaskCount = 1 };

    internal static AchievementEntity AchievementEntity =
            new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription", Type = AchievementType.Add, TaskCount = 1 };
}
