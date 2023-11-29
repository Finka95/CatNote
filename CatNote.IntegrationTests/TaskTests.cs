using System.Net.Http.Json;
using AutoFixture.Xunit2;
using CatNote.API.DTO;
using CatNote.IntegrationTests.DataForIntegrationTests;
using FluentAssertions;
using Xunit;

namespace CatNote.IntegrationTests;

public class TaskTests
{
    private HttpClient _client;

    public TaskTests()
    {
        var factory = new TestingWebAppFactory();

        _client = factory.CreateClient();
    }

    [Theory]
    [AutoData]
    public async Task Create_CorrectTaskPass_TaskDTO(int id, int userId, string userName)
    {
        //Arrange
        var userDTO = new UserDTO
        {
            Id = userId,
            UserName = userName
        };

        var achievementDTO = AchievementData.AchievementAddFirstTaskDTO;

        var taskDTO = TaskData.TaskDTO(id);

        //Act
        await _client.PostAsJsonAsync("api/Achievement", achievementDTO);
        await _client.PostAsJsonAsync("api/User", userDTO);
        var result = await _client.PostAsJsonAsync("api/Task", taskDTO);

        //Assert
        result.EnsureSuccessStatusCode();
        var task = await result.Content.ReadFromJsonAsync<TaskDTO>();

        task.Should().BeOfType<TaskDTO>();
        task?.Title.Should().Be(taskDTO.Title);
    }

    [Theory]
    [AutoData]
    public async Task Update_TaskByCorrectIdPass_TaskDTO(int id, int userId, string userName)
    {
        //Arrange
        var userDTO = new UserDTO
        {
            Id = userId,
            UserName = userName
        };

        var achievementDTO = AchievementData.AchievementCompletedFirstTaskDTO;

        var taskDTO = TaskData.TaskDTO(id);

        await _client.PostAsJsonAsync("api/Task", taskDTO);

        //Act
        await _client.PostAsJsonAsync("api/Achievement", achievementDTO);
        await _client.PostAsJsonAsync("api/User", userDTO);
        var result = await _client.PutAsJsonAsync($"api/Task", taskDTO);

        //Assert
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadFromJsonAsync<TaskDTO>();

        response.Should().BeOfType<TaskDTO>();
        response?.Id.Should().Be(id);
        response?.Title.Should().Be(taskDTO.Title);
    }

    [Theory]
    [AutoData]
    public async Task Update_TaskByIncorrectIdPass_UnsuccessfulStatusCode(int id, string title, int userId, string userName)
    {
        //Arrange
        var userDTO = new UserDTO
        {
            Id = userId,
            UserName = userName
        };

        //Act
        await _client.PostAsJsonAsync("api/User", userDTO);

        var result = await _client.PutAsJsonAsync($"api/User", new TaskDTO
        {
            Id = id,
            Title = title,
            Date = DateTime.Now,
            Status = Domain.Enums.TaskStatus.ToDo,
            UserId = userId
        });

        //Assert
        result.IsSuccessStatusCode.Should().BeFalse();
    }
}
