using CatNote.BLL.AchievementTypes;
using CatNote.DAL.Entities;
using CatNote.Domain.Enums;

namespace CatNote.Tests.Services.DataForTests;

internal static class AchievementData
{
    internal static Achievement AchievementAddModel =
        new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription", Type = AchievementType.Add, TaskCount = 1 };

    internal static AchievementEntity AchievementAddEntity =
            new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription", Type = AchievementType.Add, TaskCount = 1 };

    internal static Achievement AchievementCompletedModel =
        new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription", Type = AchievementType.Completed, TaskCount = 1 };

    internal static AchievementEntity AchievementCompletedEntity =
        new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription", Type = AchievementType.Completed, TaskCount = 1 };
}
