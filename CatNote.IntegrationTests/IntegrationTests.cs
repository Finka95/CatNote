using System.Net;
using CatNote.API;
using CatNote.API.DTO;
using FluentAssertions;
using System.Net.Http.Json;
using AutoFixture.Xunit2;
using Xunit;

namespace CatNote.IntegrationTests;

[Collection("Sequential")]
public class IntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;

    public IntegrationTests(TestingWebAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [AutoData]
    public async Task Create_CorrectUserPass_ReturnUserDTO(int id, string name)
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
    public async Task GetAll_UsersFound_ReturnListUserDTO()
    {
        //Arrange

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
    public async Task GetById_UserFound_ReturnUserDTO(int id)
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
    public async Task GetById_UserNotFound_ReturnFalseSuccessStatusCode(int id)
    {
        //Arrange

        //Act
        var result = await _client.GetAsync($"api/User/{id}");

        //Assert
        result.IsSuccessStatusCode.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task Update_UserByInCorrectIdPass_ReturnFalseSuccessStatusCode(int id, string newName)
    {
        //Arrange

        //Act
        var result = await _client.PutAsJsonAsync($"api/User", new UserDTO { Id = id, UserName = newName });

        //Assert
        result.IsSuccessStatusCode.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task Update_UserByCorrectIdPass_ReturnUserDTO(int id, string newName)
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
    public async Task Delete_UserById_ReturnSuccess(int id)
    {
        //Arrange

        //Act
        await _client.PostAsJsonAsync("api/User", new UserDTO {Id = id, UserName = $"name{id}" });
        var resultDeleteAsync = await _client.DeleteAsync($"api/User/{id}");
        var result = await _client.GetAsync($"api/User/{id}");

        //Assert
        result.IsSuccessStatusCode.Should().BeFalse();
    }
}
