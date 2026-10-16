
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;
using System.Security.Claims;


namespace RaceDay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly RaceDayDbContext _context;

    public EventsController(RaceDayDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _context.Events
            .AsNoTracking()
            .OrderBy(e => e.EventDate)
            .Select(e => new
            {
                e.EventID,
                e.EventName,
                e.Description,
                e.EventDate,
                e.Location,
                e.Distance,
                e.EventType,
                e.OrganizerID
            })
            .ToListAsync();

        return Ok(events);
    }


    [HttpPost]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> CreateEvent(
        CreateEventRequest request)
    {
        var userId = GetLoggedInUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        var organizer = await _context.Organizers
            .FirstOrDefaultAsync(o => o.UserID == userId.Value);

        if (organizer == null)
        {
            return Forbid();
        }

        var newEvent = new RaceDay.API.Models.Event
        {
            OrganizerID = organizer.OrganizerID,
            EventName = request.EventName.Trim(),
            Description = request.Description,
            EventDate = request.EventDate!.Value,
            Location = request.Location.Trim(),
            Distance = request.Distance,
            EventType = request.EventType
        };

        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();

        return Created($"/api/Events/{newEvent.EventID}", new
        {
            newEvent.EventID,
            newEvent.EventName,
            newEvent.Description,
            newEvent.EventDate,
            newEvent.Location,
            newEvent.Distance,
            newEvent.EventType,
            newEvent.OrganizerID
        });
    }


    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetEvent(int id)
    {
        var eventItem = await _context.Events
            .AsNoTracking()
            .Where(e => e.EventID == id)
            .Select(e => new
            {
                e.EventID,
                e.EventName,
                e.Description,
                e.EventDate,
                e.Location,
                e.Distance,
                e.EventType,
                e.OrganizerID
            })
            .FirstOrDefaultAsync();

        if (eventItem == null)
        {
            return NotFound(new
            {
                message = "Event not found."
            });
        }

        return Ok(eventItem);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> UpdateEvent(
        int id,
        UpdateEventRequest request)
    {
        var userId = GetLoggedInUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        var organizer = await _context.Organizers
            .FirstOrDefaultAsync(o => o.UserID == userId.Value);

        if (organizer == null)
        {
            return Forbid();
        }

        var eventItem = await _context.Events
            .FirstOrDefaultAsync(e => e.EventID == id);

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

        eventItem.EventName = request.EventName.Trim();
        eventItem.Description = request.Description;
        eventItem.EventDate = request.EventDate!.Value;
        eventItem.Location = request.Location.Trim();
        eventItem.Distance = request.Distance;
        eventItem.EventType = request.EventType;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Event updated successfully.",
            eventItem.EventID,
            eventItem.EventName,
            eventItem.Description,
            eventItem.EventDate,
            eventItem.Location,
            eventItem.Distance,
            eventItem.EventType
        });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var userId = GetLoggedInUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        var organizer = await _context.Organizers
            .FirstOrDefaultAsync(o => o.UserID == userId.Value);

        if (organizer == null)
        {
            return Forbid();
        }

        var eventItem = await _context.Events
            .FirstOrDefaultAsync(e => e.EventID == id);

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

        _context.Events.Remove(eventItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private int? GetLoggedInUserId()
    {
        var userIdValue = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (int.TryParse(userIdValue, out var userId))
        {
            return userId;
        }

        return null;
    }
}
