using System.Net.Http.Json;
using AutoFixture.Xunit2;
using CatNote.API.DTO;
using CatNote.Domain.Enums;
using CatNote.IntegrationTests.DataForIntegrationTests;
using FluentAssertions;
using Xunit;

namespace CatNote.IntegrationTests;

public class UserTests
{
    private readonly HttpClient _client;

    public UserTests()
    {
        var factory = new TestingWebAppFactory();

        _client = factory.CreateClient();
    }

    [Theory]
    [AutoData]
    public async Task Get_UsersFoundList_UserDTO(int userId1, int userId2, int achievementId1, int achievementId2, int taskId1, int taskId2)
    {
        //Arrange
        var userDTO1 = new UserDTO
        {
            Id = userId1,
            UserName = "Default1",
        };

        userDTO1.Achievements = new List<AchievementDTO>
        {
             AchievementData.AchievementAddDTOWithPoint(achievementId1, 3)
        };
        userDTO1.Tasks = new List<TaskDTO>
        {
            TaskData.TaskDTO(taskId1)
        };

        var userDTO2 = new UserDTO
        {
            Id = userId2,
            UserName = "Default2",
        };

        userDTO2.Achievements = new List<AchievementDTO>
        {
            AchievementData.AchievementAddDTOWithPoint(achievementId2, 1)
        };

        userDTO2.Tasks = new List<TaskDTO>
        {
            TaskData.TaskDTO(taskId2)
        };

        //Act
        await _client.PostAsJsonAsync("api/User", userDTO1);
        await _client.PostAsJsonAsync("api/User", userDTO2);
        var result = await _client.GetAsync("api/User/achievements/points");

        //Assert
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadFromJsonAsync<List<UserDTO>>();

        response.Should().NotBeNull();

        var rightResponse = response!.OrderByDescending(x => x.Achievements?.Select(y => y.Point).Sum()).ToList();

        response.Should().BeOfType<List<UserDTO>>();
        response.Should().Equal(rightResponse);
    }
}
