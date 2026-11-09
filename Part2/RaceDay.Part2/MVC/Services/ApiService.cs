
using System.Net.Http.Json;

namespace MVC.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private const string ApiCookieKey = "ApiSessionCookie";

    public ApiService(
        HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<HttpResponseMessage> SendAsync(
        HttpMethod method,
        string endpoint,
        object? data = null)
    {
        var request = new HttpRequestMessage(method, endpoint);

        // Forward the current user's API session cookie.
        var context = _httpContextAccessor.HttpContext;

        var apiCookie = context?.Session.GetString(ApiCookieKey);

        if (!string.IsNullOrWhiteSpace(apiCookie))
        {
            request.Headers.TryAddWithoutValidation(
                "Cookie", apiCookie);
        }

        // Attach JSON data when supplied.
        if (data != null)
        {
            request.Content = JsonContent.Create(data);
        }

        var response = await _httpClient.SendAsync(request);

        // Save the API session cookie after login.
        if (response.Headers.TryGetValues(
            "Set-Cookie", out var cookies))
        {
            foreach (var cookie in cookies)
            {
                if (cookie.Contains(
                    ".AspNetCore.Session=",
                    StringComparison.OrdinalIgnoreCase))
                {
                    var cookieValue = cookie.Split(';')[0];

                    context?.Session.SetString(
                        ApiCookieKey, cookieValue);

                    break;
                }
            }
        }

        return response;
    }

    public void ClearApiSession()
    {
        _httpContextAccessor.HttpContext?
            .Session.Remove(ApiCookieKey);
    }
}
