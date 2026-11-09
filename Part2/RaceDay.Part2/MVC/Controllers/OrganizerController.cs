
using Microsoft.AspNetCore.Mvc;
using MVC.Services;
using MVC.ViewModels;
using System.Net.Http.Json;

namespace MVC.Controllers;

public class OrganizerController : Controller
{
    private readonly ApiService _apiService;

    public OrganizerController(ApiService apiService)
    {
        _apiService = apiService;
    }

    private bool IsOrganizer()
    {
        int? userId = HttpContext.Session.GetInt32("UserID");
        string? role = HttpContext.Session.GetString("Role");

        return userId != null && role == "Organizer";
    }

    public async Task<IActionResult> Index()
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        var response = await _apiService.SendAsync(
            HttpMethod.Get, "api/Events/my");

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = "Unable to load events.";
            return View(new List<OrganizerEventViewModel>());
        }

        var events = await response.Content
            .ReadFromJsonAsync<List<OrganizerEventViewModel>>();

        return View(events ?? new List<OrganizerEventViewModel>());
    }

    [HttpGet]
    public IActionResult Create()
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        return View(new CreateOrganizerEventViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateOrganizerEventViewModel model)
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        if (!ModelState.IsValid)
            return View(model);

        var request = new
        {
            model.EventName,
            model.Description,
            model.EventDate,
            model.Location,
            model.Distance,
            model.EventType
        };

        var response = await _apiService.SendAsync(
            HttpMethod.Post, "api/Events", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            ModelState.AddModelError(
                string.Empty,
                "Unable to create event. " + error);

            return View(model);
        }

        TempData["SuccessMessage"] =
            "Your event was created successfully!";

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        var response = await _apiService.SendAsync(
            HttpMethod.Get, $"api/Events/{id}");

        if (!response.IsSuccessStatusCode)
            return NotFound();

        var eventItem = await response.Content
            .ReadFromJsonAsync<EditOrganizerEventViewModel>();

        if (eventItem == null)
            return NotFound();

        return View(eventItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditOrganizerEventViewModel model)
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        if (!ModelState.IsValid)
            return View(model);

        var request = new
        {
            model.EventName,
            model.Description,
            model.EventDate,
            model.Location,
            model.Distance,
            model.EventType
        };

        var response = await _apiService.SendAsync(
            HttpMethod.Put,
            $"api/Events/{model.EventID}",
            request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            ModelState.AddModelError(
                string.Empty,
                "Unable to update event. " + error);

            return View(model);
        }

        TempData["SuccessMessage"] =
            "Event updated successfully!";

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        var response = await _apiService.SendAsync(
            HttpMethod.Delete, $"api/Events/{id}");

        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] =
                "Unable to delete event. You may not own this event.";

            return RedirectToAction("Index");
        }

        TempData["SuccessMessage"] =
            "Event deleted successfully!";

        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Categories(int eventId)
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        var response = await _apiService.SendAsync(
            HttpMethod.Get, $"api/Categories/event/{eventId}");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            TempData["ErrorMessage"] =
                $"Unable to load categories. API Status: {(int)response.StatusCode}. Details: {error}";

            return RedirectToAction("Index");
        }

        var categories = await response.Content
            .ReadFromJsonAsync<List<OrganizerCategoryViewModel>>();

        ViewBag.EventID = eventId;

        return View(categories ?? new List<OrganizerCategoryViewModel>());
    }
    [HttpGet]
    public IActionResult CreateCategory(int eventId)
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        return View(new CreateOrganizerCategoryViewModel
        {
            EventID = eventId
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory(
        CreateOrganizerCategoryViewModel model)
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        if (model.MinAge.HasValue &&
            model.MaxAge.HasValue &&
            model.MinAge > model.MaxAge)
        {
            ModelState.AddModelError(
                "", "Minimum age cannot exceed maximum age.");
        }

        if (model.MinDistance.HasValue &&
            model.MaxDistance.HasValue &&
            model.MinDistance > model.MaxDistance)
        {
            ModelState.AddModelError(
                "", "Minimum distance cannot exceed maximum distance.");
        }

        if (!ModelState.IsValid)
            return View(model);

        var request = new
        {
            model.CategoryName,
            model.Description,
            model.MinAge,
            model.MaxAge,
            model.MinDistance,
            model.MaxDistance,
            model.SortOrder
        };

        var response = await _apiService.SendAsync(
            HttpMethod.Post,
            $"api/Categories/event/{model.EventID}",
            request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            ModelState.AddModelError(
                "", "Unable to create category. " + error);

            return View(model);
        }

        TempData["SuccessMessage"] =
            "Category created successfully!";

        return RedirectToAction(
            "Categories",
            new { eventId = model.EventID });
    }
    [HttpGet]
    public async Task<IActionResult> EditCategory(int categoryId, int eventId)
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        var response = await _apiService.SendAsync(
            HttpMethod.Get, $"api/Categories/event/{eventId}");

        if (!response.IsSuccessStatusCode)
            return NotFound();

        var categories = await response.Content
            .ReadFromJsonAsync<List<OrganizerCategoryViewModel>>();

        var category = categories?.FirstOrDefault(
            c => c.CategoryID == categoryId);

        if (category == null)
            return NotFound();

        return View(category);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCategory(
    OrganizerCategoryViewModel model)
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        if (model.MinAge.HasValue &&
            model.MaxAge.HasValue &&
            model.MinAge > model.MaxAge)
        {
            ModelState.AddModelError(
                "", "Minimum age cannot exceed maximum age.");
        }

        if (model.MinDistance.HasValue &&
            model.MaxDistance.HasValue &&
            model.MinDistance > model.MaxDistance)
        {
            ModelState.AddModelError(
                "", "Minimum distance cannot exceed maximum distance.");
        }

        if (!ModelState.IsValid)
            return View(model);

        var request = new
        {
            model.CategoryName,
            model.Description,
            model.MinAge,
            model.MaxAge,
            model.MinDistance,
            model.MaxDistance,
            model.SortOrder
        };

        var response = await _apiService.SendAsync(
            HttpMethod.Put,
            $"api/Categories/{model.CategoryID}",
            request);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(
                "", "Unable to update category. " +
                await response.Content.ReadAsStringAsync());

            return View(model);
        }

        TempData["SuccessMessage"] = "Category updated successfully!";

        return RedirectToAction(
            "Categories",
            new { eventId = model.EventID });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(int categoryId, int eventId)
    {
        if (!IsOrganizer())
            return RedirectToAction("Login", "Account");

        var response = await _apiService.SendAsync(
            HttpMethod.Delete, $"api/Categories/{categoryId}");

        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = "Unable to delete category.";
        }
        else
        {
            TempData["SuccessMessage"] = "Category deleted successfully!";
        }

        return RedirectToAction("Categories", new { eventId });
    }
}
