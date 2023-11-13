using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.Tests.Services.DataForTests;

public static class AchievementData
{
    public static AchievementModel GetAchievementModel() => new()
    {
        Id = 1,
        Title = "defaultTitle",
        Description = "defaultDescription"
    };

    public static AchievementEntity GetAchievementEntity() => new()
    {
        Id = 1,
        Title = "defaultTitle",
        Description = "defaultDescription"
    };
}
