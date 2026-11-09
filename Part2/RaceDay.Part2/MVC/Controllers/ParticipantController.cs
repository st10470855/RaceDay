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
                return RedirectToAction("Login", "Account");

            return View();
        }

        public async Task<IActionResult> Events()
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            var response = await _apiService.SendAsync(
                HttpMethod.Get, "api/Events");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Unable to load events.";
                return View(new List<ParticipantEventViewModel>());
            }

            var events = await response.Content
                .ReadFromJsonAsync<List<ParticipantEventViewModel>>();

            return View(events ?? new List<ParticipantEventViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Enrol(int id)
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            var eventResponse = await _apiService.SendAsync(
                HttpMethod.Get, $"api/Events/{id}");

            if (!eventResponse.IsSuccessStatusCode)
                return NotFound();

            var eventItem = await eventResponse.Content
                .ReadFromJsonAsync<ParticipantEventViewModel>();

            if (eventItem == null)
                return NotFound();

            var categoryResponse = await _apiService.SendAsync(
                HttpMethod.Get, $"api/Categories/event/{id}");

            if (!categoryResponse.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] =
                    "Unable to load event categories.";

                return RedirectToAction("Events");
            }

            var categories = await categoryResponse.Content
                .ReadFromJsonAsync<List<ParticipantCategoryViewModel>>();

            var model = new CreateEnrolmentViewModel
            {
                EventID = id,
                EventName = eventItem.EventName,
                Categories = categories ?? new List<ParticipantCategoryViewModel>()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enrol(CreateEnrolmentViewModel model)
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                var categoryResponse = await _apiService.SendAsync(
                    HttpMethod.Get,
                    $"api/Categories/event/{model.EventID}");

                if (categoryResponse.IsSuccessStatusCode)
                {
                    model.Categories =
                        await categoryResponse.Content
                            .ReadFromJsonAsync<List<ParticipantCategoryViewModel>>()
                        ?? new List<ParticipantCategoryViewModel>();
                }

                return View(model);
            }

            var request = new
            {
                model.EventID,
                model.CategoryID
            };

            var response = await _apiService.SendAsync(
                HttpMethod.Post, "api/Enrolments", request);

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] =
                    response.StatusCode == System.Net.HttpStatusCode.Conflict
                    ? "You are already enrolled in this event."
                    : "Enrolment failed. Please check the selected category.";

                return RedirectToAction("Enrol", new { id = model.EventID });
            }

            TempData["SuccessMessage"] =
                "You have successfully enrolled in the event!";

            return RedirectToAction("Events");
        }
    }
}