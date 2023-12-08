using System.Net.Http.Json;
using AutoFixture.Xunit2;
using CatNote.API.DTO;
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
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImVRR3BlLVF5WjZPeWFWSGc0dlFFViJ9.eyJpc3MiOiJodHRwczovL2Rldi11anh2ZW5hb21raWRiMzZoLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJhdXRoMHw2NTY1ZjBmYjg3Y2IzMDAzYzg3OTc4YWUiLCJhdWQiOlsiaHR0cHM6Ly9sb2NhbGhvc3Q6NzE2NS9hcGkiLCJodHRwczovL2Rldi11anh2ZW5hb21raWRiMzZoLnVzLmF1dGgwLmNvbS91c2VyaW5mbyJdLCJpYXQiOjE3MDIwMTc3NTIsImV4cCI6MTcwMjEwNDE1MiwiYXpwIjoia25maXhKV2tQZFFib2hjTjJKNkRFZjVvc1N4WTE1N3EiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIiwicGVybWlzc2lvbnMiOlsicmVhZDptZXNzYWdlcyJdfQ.Pvg_PkS2pg2zfFuT1n51efWjYOfy_ui7zxZwTb3pCj-fmsF4_k7iIe65bSv6lxDSAQ43qDIxiOwIc5Qu6bH-Q2mrk_M4OMM1L1b8kDxdma1nHu2EX1NYvY3wVAB2ZQoaCKtCPpcJVUneMhaWhME6o-NboYC5GSyFwPlGhHtNFgvlEsqpxle2_judjfdTLa46wR8fw_MLDBBQgKEHzlgIU52C2_x0kd1ybu_4bSwtxyBfK3AvbXF2RgkDFkcqN68DHshgEx8FmyrmTD8C9yydg2ndZx53Cq5E7dHMyN6BQ0Pd_TeVHyARsCLOxZ0m4j01eY9OcTA5bQr-tyxAGIPkDQ");
    }

    [Theory]
    [AutoData]
    public async Task Get_UsersFoundList_UserDTO(int userId1, int userId2, int achievementId1, int achievementId2, int taskId1, int taskId2)
    {
        //Arrange
        var userDTO1 = UserData.UserWithTaskAchievement(userId1, achievementId1, 3, taskId1);

        var userDTO2 = UserData.UserWithTaskAchievement(userId2, achievementId2, 1, taskId2);

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
