using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.Tests.Services.DataForTests;

public static class AchievementData
{
    public static AchievementModel AchievementModel =
        new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription" };

    public static AchievementEntity AchievementEntity =
        new() { Id = 1, Title = "defaultTitle", Description = "defaultDescription" };
}
