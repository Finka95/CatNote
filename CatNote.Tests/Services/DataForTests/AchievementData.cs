using CatNote.BLL.Models;
using CatNote.DAL.Entities;
using CatNote.Domain.Enums;

namespace CatNote.Tests.Services.DataForTests;

internal static class AchievementData
{
    internal static AchievementModel AchievementAddModel =
        new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription", Type = AchievementType.Add, TaskCount = 1 };

    internal static AchievementEntity AchievementAddEntity =
            new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription", Type = AchievementType.Add, TaskCount = 1 };

    internal static AchievementModel AchievementCompletedModel =
        new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription", Type = AchievementType.Completed, TaskCount = 1 };

    internal static AchievementEntity AchievementCompletedEntity =
        new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription", Type = AchievementType.Completed, TaskCount = 1 };
}
