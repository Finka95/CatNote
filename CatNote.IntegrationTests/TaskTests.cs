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
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImVRR3BlLVF5WjZPeWFWSGc0dlFFViJ9.eyJpc3MiOiJodHRwczovL2Rldi11anh2ZW5hb21raWRiMzZoLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJhdXRoMHw2NTY1ZjBmYjg3Y2IzMDAzYzg3OTc4YWUiLCJhdWQiOlsiaHR0cHM6Ly9sb2NhbGhvc3Q6NzE2NS9hcGkiLCJodHRwczovL2Rldi11anh2ZW5hb21raWRiMzZoLnVzLmF1dGgwLmNvbS91c2VyaW5mbyJdLCJpYXQiOjE3MDIwMTc3NTIsImV4cCI6MTcwMjEwNDE1MiwiYXpwIjoia25maXhKV2tQZFFib2hjTjJKNkRFZjVvc1N4WTE1N3EiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIiwicGVybWlzc2lvbnMiOlsicmVhZDptZXNzYWdlcyJdfQ.Pvg_PkS2pg2zfFuT1n51efWjYOfy_ui7zxZwTb3pCj-fmsF4_k7iIe65bSv6lxDSAQ43qDIxiOwIc5Qu6bH-Q2mrk_M4OMM1L1b8kDxdma1nHu2EX1NYvY3wVAB2ZQoaCKtCPpcJVUneMhaWhME6o-NboYC5GSyFwPlGhHtNFgvlEsqpxle2_judjfdTLa46wR8fw_MLDBBQgKEHzlgIU52C2_x0kd1ybu_4bSwtxyBfK3AvbXF2RgkDFkcqN68DHshgEx8FmyrmTD8C9yydg2ndZx53Cq5E7dHMyN6BQ0Pd_TeVHyARsCLOxZ0m4j01eY9OcTA5bQr-tyxAGIPkDQ");
    }

    [Theory]
    [AutoData]
    public async Task Create_CorrectTaskPass_TaskDTO(int id, int userId, string userName)
    {
        //Arrange

        var userDTO = UserData.UserDTO(userId, userName);

        var achievementDTO = AchievementData.AchievementAddFirstTaskDTO;

        var taskDTO = TaskData.TaskDTO(id, userId);

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
        var userDTO = UserData.UserDTO(userId, userName);

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
        var userDTO = UserData.UserDTO(userId, userName);

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
