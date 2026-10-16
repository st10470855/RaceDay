using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace RaceDay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly RaceDayDbContext _context;

    public CategoriesController(RaceDayDbContext context)
    {
        _context = context;
    }

    [HttpGet("event/{eventId:int}")]
    public async Task<IActionResult> GetCategoriesForEvent(int eventId)
    {
        var eventExists = await _context.Events
            .AnyAsync(e => e.EventID == eventId);

        if (!eventExists)
        {
            return NotFound(new
            {
                message = "Event not found."
            });
        }

        var categories = await _context.Categories
            .AsNoTracking()
            .Where(c => c.EventID == eventId)
            .OrderBy(c => c.SortOrder)
            .Select(c => new
            {
                c.CategoryID,
                c.EventID,
                c.CategoryName,
                c.Description,
                c.MinAge,
                c.MaxAge,
                c.MinDistance,
                c.MaxDistance,
                c.SortOrder
            })
            .ToListAsync();

        return Ok(categories);
    }
    [HttpPost("event/{eventId:int}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> CreateCategory(
    int eventId,
    RaceDay.API.DTOs.CreateCategoryRequest request)
    {
        var userIdValue = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out var userId))
        {
            return Unauthorized();
        }

        if (request.MinAge.HasValue &&
            request.MaxAge.HasValue &&
            request.MinAge > request.MaxAge)
        {
            return BadRequest(new
            {
                message = "Minimum age cannot exceed maximum age."
            });
        }

        if (request.MinDistance.HasValue &&
            request.MaxDistance.HasValue &&
            request.MinDistance > request.MaxDistance)
        {
            return BadRequest(new
            {
                message = "Minimum distance cannot exceed maximum distance."
            });
        }

        var organizer = await _context.Organizers
            .FirstOrDefaultAsync(o => o.UserID == userId);

        if (organizer == null)
        {
            return Forbid();
        }

        var eventItem = await _context.Events
            .FirstOrDefaultAsync(e => e.EventID == eventId);

        if (eventItem == null)
        {
            return NotFound(new
            {
                message = "Event not found."
            });
        }

        if (eventItem.OrganizerID != organizer.OrganizerID)
        {
            return Forbid();
        }

        var category = new RaceDay.API.Models.Category
        {
            EventID = eventId,
            CategoryName = request.CategoryName.Trim(),
            Description = request.Description,
            MinAge = request.MinAge,
            MaxAge = request.MaxAge,
            MinDistance = request.MinDistance,
            MaxDistance = request.MaxDistance,
            SortOrder = request.SortOrder
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Created(
            $"/api/Categories/event/{eventId}",
            new
            {
                category.CategoryID,
                category.EventID,
                category.CategoryName,
                category.Description,
                category.MinAge,
                category.MaxAge,
                category.MinDistance,
                category.MaxDistance,
                category.SortOrder
            });
    }

    [HttpPut("{categoryId:int}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> UpdateCategory(
    int categoryId,
    RaceDay.API.DTOs.UpdateCategoryRequest request)
    {
        var userIdValue = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out var userId))
        {
            return Unauthorized();
        }

        if (request.MinAge.HasValue &&
            request.MaxAge.HasValue &&
            request.MinAge > request.MaxAge)
        {
            return BadRequest(new
            {
                message = "Minimum age cannot exceed maximum age."
            });
        }

        if (request.MinDistance.HasValue &&
            request.MaxDistance.HasValue &&
            request.MinDistance > request.MaxDistance)
        {
            return BadRequest(new
            {
                message = "Minimum distance cannot exceed maximum distance."
            });
        }

        var organizer = await _context.Organizers
            .FirstOrDefaultAsync(o => o.UserID == userId);

        if (organizer == null)
        {
            return Forbid();
        }

        var category = await _context.Categories
            .Include(c => c.Event)
            .FirstOrDefaultAsync(c => c.CategoryID == categoryId);

        if (category == null)
        {
            return NotFound(new
            {
                message = "Category not found."
            });
        }

        if (category.Event.OrganizerID != organizer.OrganizerID)
        {
            return Forbid();
        }

        category.CategoryName = request.CategoryName.Trim();
        category.Description = request.Description;
        category.MinAge = request.MinAge;
        category.MaxAge = request.MaxAge;
        category.MinDistance = request.MinDistance;
        category.MaxDistance = request.MaxDistance;
        category.SortOrder = request.SortOrder;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Category updated successfully.",
            category.CategoryID,
            category.EventID,
            category.CategoryName,
            category.Description,
            category.MinAge,
            category.MaxAge,
            category.MinDistance,
            category.MaxDistance,
            category.SortOrder
        });
    }
    [HttpDelete("{categoryId:int}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        var userIdValue = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out var userId))
        {
            return Unauthorized();
        }

        var organizer = await _context.Organizers
            .FirstOrDefaultAsync(o => o.UserID == userId);

        if (organizer == null)
        {
            return Forbid();
        }

        var category = await _context.Categories
            .Include(c => c.Event)
            .FirstOrDefaultAsync(c => c.CategoryID == categoryId);

        if (category == null)
        {
            return NotFound(new
            {
                message = "Category not found."
            });
        }

        if (category.Event.OrganizerID != organizer.OrganizerID)
        {
            return Forbid();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}