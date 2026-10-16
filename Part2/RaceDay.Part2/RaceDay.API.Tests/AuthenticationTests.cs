using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace RaceDay.API.Tests;

public class AuthenticationTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthenticationTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginData = new
        {
            email = "nobody@example.com",
            password = "WrongPassword123!"
        };

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(loginData),
            System.Text.Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync(
            "/api/Auth/login", content);

        // Assert
        Assert.Equal(
            HttpStatusCode.Unauthorized,
            response.StatusCode);
    }
}