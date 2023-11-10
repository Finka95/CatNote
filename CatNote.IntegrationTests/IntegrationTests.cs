using System.Net;
using CatNote.API;
using CatNote.API.DTO;
using FluentAssertions;
using System.Net.Http.Json;
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
    [InlineData(1, "name1")]
    [InlineData(2, "name2")]
    [InlineData(3, "name3")]
    public async Task IntegrationTestsCreate(int id, string name)
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
        user.Id.Should().Be(id);
        
    }

    [Fact]
    public async Task IntegrationTestsGetAll()
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
    [InlineData(7)]
    public async Task IntegrationTestsGetById(int id)
    {
        //Arrange
        var name = $"name{id}";

        //Act
        await _client.PostAsJsonAsync("api/User", new UserDTO { Id = 7, UserName = "name7" });

        var result = await _client.GetAsync($"api/User/{id}");

        //Assert
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadFromJsonAsync<UserDTO>();

        response.Should().BeOfType<UserDTO>().Which.Id.Should().Be(id);
        response.UserName.Should().Be(name);
    }

    [Theory]
    [InlineData(8, "name4")]
    public async Task IntegrationTestsUpdate(int id, string newName)
    {
        //Arrange
        await _client.PostAsJsonAsync("api/User", new UserDTO { Id = 8, UserName = "name8" });

        //Act
        var result = await _client.PutAsJsonAsync($"api/User", new UserDTO { Id = id, UserName = newName });

        //Assert
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadFromJsonAsync<UserDTO>();

        response.Should().BeOfType<UserDTO>();
        response.Id.Should().Be(id);
        response.UserName.Should().Be(newName);
    }

    [Theory]
    [InlineData(9)]
    public async Task IntegrationTestsDelete(int id)
    {
        //Arrange

        //Act
        await _client.PostAsJsonAsync("api/User", new UserDTO { Id = 9, UserName = "name9" });
        var resultDeleteA = await _client.DeleteAsync($"api/User/{id}");
        var result = await _client.GetAsync($"api/User/{id}");

        //Assert
        result.Should().BeNull();
    }
}
