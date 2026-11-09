
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

        [HttpGet]
        public async Task<IActionResult> Events(
            string? eventType,
            string? location)
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
                .ReadFromJsonAsync<List<ParticipantEventViewModel>>()
                ?? new List<ParticipantEventViewModel>();

            if (!string.IsNullOrWhiteSpace(eventType))
            {
                events = events
                    .Where(e => e.EventType.Equals(
                        eventType,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                events = events
                    .Where(e => e.Location.Contains(
                        location,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.SelectedEventType = eventType;
            ViewBag.SelectedLocation = location;

            return View(events);
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
        public async Task<IActionResult> Enrol(
            CreateEnrolmentViewModel model)
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

        public async Task<IActionResult> MyEvents()
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            var response = await _apiService.SendAsync(
                HttpMethod.Get, "api/Enrolments/my");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage =
                    "Unable to load your event registrations.";

                return View(new List<MyEnrolmentViewModel>());
            }

            var enrolments = await response.Content
                .ReadFromJsonAsync<List<MyEnrolmentViewModel>>();

            return View(enrolments ?? new List<MyEnrolmentViewModel>());
        }

        public async Task<IActionResult> Results()
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            var response = await _apiService.SendAsync(
                HttpMethod.Get, "api/Results/my");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage =
                    "Unable to load your race results.";

                return View(new List<ParticipantResultViewModel>());
            }

            var results = await response.Content
                .ReadFromJsonAsync<List<ParticipantResultViewModel>>();

            return View(results ?? new List<ParticipantResultViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            var response = await _apiService.SendAsync(
                HttpMethod.Get, "api/Profile");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Unable to load your profile.";
                return View(new ParticipantProfileViewModel());
            }

            var profile = await response.Content
                .ReadFromJsonAsync<ParticipantProfileViewModel>();

            return View(profile ?? new ParticipantProfileViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(
            ParticipantProfileViewModel model)
        {
            if (!IsParticipant())
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(model);

            var request = new
            {
                model.FirstName,
                model.LastName,
                model.Email,
                model.Phone,
                model.DateOfBirth,
                model.Gender,
                model.Club
            };

            var response = await _apiService.SendAsync(
                HttpMethod.Put, "api/Profile", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                ModelState.AddModelError(
                    string.Empty,
                    "Unable to update your profile. " + error);

                return View(model);
            }

            HttpContext.Session.SetString("FirstName", model.FirstName);
            HttpContext.Session.SetString("Email", model.Email);

            TempData["SuccessMessage"] =
                "Your profile has been updated successfully.";

            return RedirectToAction("Profile");
        }
    }
}
