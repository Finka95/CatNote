using CatNote.API.DTO;
using CatNote.Domain.Enums;

namespace CatNote.IntegrationTests.DataForIntegrationTests;

internal static class AchievementData
{
    internal static AchievementDTO AchievementAddFirstTaskDTO =
        new() { Id = 1, Title = "First task", Description = "Add first task", Type = AchievementType.Add, TaskCount = 1, Point = 1 };

    internal static AchievementDTO AchievementCompletedFirstTaskDTO =
        new() { Id = 2, Title = "Completed first task", Description = "Completed first task", Type = AchievementType.Completed, TaskCount = 1, Point = 2 };

    internal static AchievementDTO AchievementAddDTOWithPoint(int id, int point) =>
        new()
        {
            Id = id, Title = $"title{id}", Description = $"description{id}", Point = point, Type = AchievementType.Add, TaskCount = 1
        };

    internal static AchievementDTO AchievementCompletedDTOWithPoint(int id, int point) =>
        new()
        {
            Id = id, Title = $"title{id}", Description = $"description{id}", Point = point, Type = AchievementType.Completed, TaskCount = 1
        };
}
