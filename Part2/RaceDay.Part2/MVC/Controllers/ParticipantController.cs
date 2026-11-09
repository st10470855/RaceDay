using Microsoft.AspNetCore.Mvc;
using MVC.Services;
using MVC.ViewModels;
using System.Net.Http.Json;

namespace MVC.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly ApiService _apiService;

        public ParticipantController(ApiService apiService)
        {
            _apiService = apiService;
        }

        private bool IsParticipant()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            string? role = HttpContext.Session.GetString("Role");

            return userId != null && role == "Participant";
        }

        public IActionResult Index()
        {
            if (!IsParticipant())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public async Task<IActionResult> Events()
        {
            if (!IsParticipant())
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _apiService.SendAsync(
                HttpMethod.Get, "api/Events");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage =
                    "Unable to load events. Please try again.";

                return View(new List<ParticipantEventViewModel>());
            }

            var events = await response.Content
                .ReadFromJsonAsync<List<ParticipantEventViewModel>>();

            return View(events ?? new List<ParticipantEventViewModel>());
        }
    }
}