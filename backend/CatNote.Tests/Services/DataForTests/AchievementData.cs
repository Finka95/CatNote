using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.Tests.Services.DataForTests;

internal static class AchievementData
{
    internal static AchievementModel AchievementModel =
        new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription" };

    internal static AchievementEntity AchievementEntity =
        new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription" };
}
