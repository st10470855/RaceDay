
using System.Net;
using Xunit;

namespace RaceDay.API.Tests;

public class AuthenticationTests
    : IClassFixture<RaceDayTestFactory>
{
    private readonly HttpClient _client;

    public AuthenticationTests(RaceDayTestFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        var loginData = new
        {
            email = "nobody@example.com",
            password = "WrongPassword123!"
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(loginData),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync(
            "/api/Auth/login",
            content);

        var responseBody = await response.Content.ReadAsStringAsync();

        Assert.True(
            response.StatusCode == HttpStatusCode.Unauthorized,
            $"Expected 401 Unauthorized, but got {(int)response.StatusCode} ({response.StatusCode}). Response: {responseBody}");
    }

    [Fact]
    public async Task Register_WithMissingRequiredFields_ReturnsBadRequest()
    {
        var content = new StringContent(
            "{}",
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync(
            "/api/Auth/register",
            content);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }
}
