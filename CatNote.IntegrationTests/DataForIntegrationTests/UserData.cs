using CatNote.API.DTO;

namespace CatNote.IntegrationTests.DataForIntegrationTests;

public static class UserData
{
    internal static UserDTO UserWithTaskAchievement(int id, int achievementId, int achievementPoint, int taskId) =>
        new()
        {
            Id = id, UserName = "defaultUserName",
            Achievements = new List<AchievementDTO>
                { AchievementData.AchievementAddDTOWithPoint(achievementId, achievementPoint) },
            Tasks = new List<TaskDTO>
            {
                TaskData.TaskDTO(taskId)
            }
        };

    internal static UserDTO UserDTO(int id, string userName) =>
        new() { Id = id, UserName = userName };
}
