using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
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

    [Fact]
    public async Task Get_UsersFoundList_UserDTO()
    {
        //Arrange
        var userDTO1 = new UserDTO()
        {
            Id = 1,
            UserName = "Default1",
            Achievements = new List<AchievementDTO>
            {
                new ()
                {
                    Id = 1,
                    Title = "Title1",
                    Description = "Description1",
                    Point = 1,
                    Type = AchievementType.Add,
                    TaskCount = 1
                },
                new ()
                {
                    Id = 2,
                    Title = "Title2",
                    Description = "Description2",
                    Point = 3,
                    Type = AchievementType.Add,
                    TaskCount = 1
                }
            },
            Tasks = new List<TaskDTO>
            {
                new ()
                {
                    Id = 1,
                    Title = "Title",
                    Status = TaskStatus.ToDo,
                    Date = DateTime.Today
                }
            }
        };

        var userDTO2 = new UserDTO()
        {
            Id = 2,
            UserName = "Default2",
            Achievements = new List<AchievementDTO>
            {
                new ()
                {
                    Id = 1,
                    Title = "Title1",
                    Description = "Description1",
                    Point = 1,
                    Type = AchievementType.Add,
                    TaskCount = 1
                }
            },
            Tasks = new List<TaskDTO>
            {
                new ()
                {
                    Id = 1,
                    Title = "Title",
                    Status = TaskStatus.ToDo,
                    Date = DateTime.Today
                }
            }
        };

        //Act
        await _client.PostAsJsonAsync("api/User", userDTO1);
        await _client.PostAsJsonAsync("api/User", userDTO2);
        var result = await _client.GetAsync("api/User/achievements/points");

        //Assert
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadFromJsonAsync<List<UserDTO>>();

        response!.First().Id.Should().Be(userDTO1.Id);
        response!.First().UserName.Should().Be(userDTO1.UserName);
        response!.Last().Id.Should().Be(userDTO2.Id);
        response!.Last().UserName.Should().Be(userDTO2.UserName);
    }
}
