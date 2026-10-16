
using System.Net;
using Xunit;

namespace RaceDay.API.Tests;

public class ApiAuthorizationTests
    : IClassFixture<RaceDayTestFactory>
{
    private readonly HttpClient _client;

    public ApiAuthorizationTests(RaceDayTestFactory factory)
    {
        _client = factory.CreateClient();
    }

    private static async Task AssertUnauthorized(
        HttpResponseMessage response)
    {
        var body = await response.Content.ReadAsStringAsync();

        Assert.True(
            response.StatusCode == HttpStatusCode.Unauthorized,
            $"Expected 401 Unauthorized, but received " +
            $"{(int)response.StatusCode} ({response.StatusCode}). " +
            $"Response: {body}");
    }

    [Fact]
    public async Task GetMyProfile_WithoutSession_ReturnsUnauthorized()
    {
        // ProfileController uses GET /api/Profile
        var response = await _client.GetAsync("/api/Profile");

        await AssertUnauthorized(response);
    }

    [Fact]
    public async Task GetMyResults_WithoutSession_ReturnsUnauthorized()
    {
        var response = await _client.GetAsync("/api/Results/my");

        await AssertUnauthorized(response);
    }

    [Fact]
    public async Task CreateEnrolment_WithoutSession_ReturnsUnauthorized()
    {
        var content = new StringContent(
            "{}",
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync(
            "/api/Enrolments",
            content);

        await AssertUnauthorized(response);
    }

    [Fact]
    public async Task RecordResult_WithoutSession_ReturnsUnauthorized()
    {
        var content = new StringContent(
            "{}",
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync(
            "/api/Results",
            content);

        await AssertUnauthorized(response);
    }
}
