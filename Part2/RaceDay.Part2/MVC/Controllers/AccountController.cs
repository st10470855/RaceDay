
using Microsoft.AspNetCore.Mvc;
using MVC.Services;
using MVC.ViewModels;
using System.Net.Http.Json;

namespace MVC.Controllers;

public class AccountController : Controller
{
    private readonly ApiService _apiService;

    public AccountController(ApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var response = await _apiService.SendAsync(
            HttpMethod.Post, "api/Auth/login", model);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(
                string.Empty,
                "Invalid email or password. Please try again.");

            return View(model);
        }

        var user = await response.Content
            .ReadFromJsonAsync<LoginResponse>();

        if (user == null)
        {
            ModelState.AddModelError(
                string.Empty, "Unable to read the login response.");

            return View(model);
        }

        HttpContext.Session.SetInt32("UserID", user.UserID);
        HttpContext.Session.SetString("FirstName", user.FirstName);
        HttpContext.Session.SetString("Role", user.Role);
        HttpContext.Session.SetString("Email", user.Email);

        if (user.Role == "Organizer")
            return RedirectToAction("Index", "Organizer");

        return RedirectToAction("Index", "Participant");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (model.Gender == "Prefer not to say")
        {
            model.Gender = "Private";
            ModelState.Remove(nameof(model.Gender));
        }

        if (!ModelState.IsValid)
            return View(model);

        var response = await _apiService.SendAsync(
            HttpMethod.Post, "api/Auth/register", model);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine("REGISTRATION API RESPONSE:");
            Console.WriteLine($"Status: {(int)response.StatusCode}");
            Console.WriteLine($"Details: {error}");

            ModelState.AddModelError(
                string.Empty,
                "Registration failed. " + error);

            return View(model);
        }

        TempData["SuccessMessage"] =
            "Registration successful! Please log in.";

        return RedirectToAction("Login");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _apiService.SendAsync(
            HttpMethod.Post, "api/Auth/logout");

        _apiService.ClearApiSession();

        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }

    private class LoginResponse
    {
        public int UserID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
