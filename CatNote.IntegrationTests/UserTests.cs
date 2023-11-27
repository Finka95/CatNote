using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using CatNote.API.DTO;
using CatNote.Domain.Enums;
using FluentAssertions;
using Xunit;
using TaskStatus = CatNote.Domain.Enums.TaskStatus;

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

        var achievement1 = new AchievementDTO
        {
            Id = achievementId1,
            Title = "Title1",
            Description = "Description1",
            Point = 3,
            Type = AchievementType.Add,
            TaskCount = 1
        };

        var task1 = new TaskDTO
        {
            Id = taskId1,
            Title = "Title",
            Status = TaskStatus.ToDo,
            Date = DateTime.Today
        };

        userDTO1.Achievements = new List<AchievementDTO>
        {
            achievement1
        };
        userDTO1.Tasks = new List<TaskDTO>
        {
            task1
        };

        var userDTO2 = new UserDTO
        {
            Id = userId2,
            UserName = "Default2",
        };

        var achievement2 = new AchievementDTO
        {
            Id = achievementId2,
            Title = "Title1",
            Description = "Description1",
            Point = 1,
            Type = AchievementType.Add,
            TaskCount = 1
        };

        var task2 = new TaskDTO
        {
            Id = taskId2,
            Title = "Title",
            Status = TaskStatus.ToDo,
            Date = DateTime.Today
        };

        userDTO2.Achievements = new List<AchievementDTO>
        {
            achievement2
        };

        userDTO2.Tasks = new List<TaskDTO>
        {
            task2
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
