using AutoFixture.Xunit2;
using CatNote.API.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace CatNote.IntegrationTests;

[Collection("Sequential")]
public class UserGenericTests
{
    private readonly HttpClient _client;

    public UserGenericTests()
    {
        var factory = new TestingWebAppFactory();

        _client = factory.CreateClient();
    }

    [Theory]
    [AutoData]
    public async Task Create_CorrectUserPass_UserDTO(int id, string name)
    {
        //Arrange
        var userDTO = new UserDTO
        {
            Id = id,
            UserName = name
        };

        //Act
        var result = await _client.PostAsJsonAsync("api/User", userDTO);

        //Assert
        result.EnsureSuccessStatusCode();
        var user = await result.Content.ReadFromJsonAsync<UserDTO>();

        user.Should().BeOfType<UserDTO>().Which.UserName.Should().Be(name);
        user?.Id.Should().Be(id);
    }

    [Fact]
    public async Task GetAll_UsersFound_ListUserDTO()
    {
        //Act
        await _client.PostAsJsonAsync("api/User", new UserDTO{Id = 4, UserName = "name4"});
        await _client.PostAsJsonAsync("api/User", new UserDTO { Id = 5, UserName = "name5" });
        await _client.PostAsJsonAsync("api/User", new UserDTO { Id = 6, UserName = "name6" });

        var result = await _client.GetAsync("api/User");

        //Assert
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadFromJsonAsync<List<UserDTO>>();

        response.Should().BeOfType<List<UserDTO>>()
            .And.Contain(x => x.Id == 4)
            .And.Contain(x => x.Id == 5)
            .And.Contain(x => x.Id == 6)
            .And.Contain(x => x.UserName == "name4")
            .And.Contain(x => x.UserName == "name5")
            .And.Contain(x => x.UserName == "name6");
    }

    [Theory]
    [AutoData]
    public async Task GetById_UserFound_UserDTO(int id)
    {
        //Arrange
        var name = $"name{id}";

        //Act
        await _client.PostAsJsonAsync("api/User", new UserDTO { Id = id, UserName = $"name{id}" });

        var result = await _client.GetAsync($"api/User/{id}");

        //Assert
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadFromJsonAsync<UserDTO>();

        response.Should().BeOfType<UserDTO>().Which.Id.Should().Be(id);
        response?.UserName.Should().Be(name);
    }

    [Theory]
    [AutoData]
    public async Task GetById_UserNotFound_StatusCodeNoContent(int id)
    {
        //Act
        var result = await _client.GetAsync($"api/User/{id}");

        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [AutoData]
    public async Task Update_UserByInCorrectIdPass_UnsuccessfulStatusCode(int id, string newName)
    {
        //Act
        var result = await _client.PutAsJsonAsync($"api/User", new UserDTO { Id = id, UserName = newName });

        //Assert
        result.IsSuccessStatusCode.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task Update_UserByCorrectIdPass_UserDTO(int id, string newName)
    {
        //Arrange
        await _client.PostAsJsonAsync("api/User", new UserDTO { Id = id, UserName = $"name{id}" });

        //Act
        var result = await _client.PutAsJsonAsync($"api/User", new UserDTO { Id = id, UserName = newName });

        //Assert
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadFromJsonAsync<UserDTO>();

        response.Should().BeOfType<UserDTO>();
        response?.Id.Should().Be(id);
        response?.UserName.Should().Be(newName);
    }

    [Theory]
    [AutoData]
    public async Task Delete_UserById_StatusCodeNoContent(int id)
    {
        //Act
        await _client.PostAsJsonAsync("api/User", new UserDTO {Id = id, UserName = $"name{id}" });
        var resultDeleteAsync = await _client.DeleteAsync($"api/User/{id}");
        var result = await _client.GetAsync($"api/User/{id}");

        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
