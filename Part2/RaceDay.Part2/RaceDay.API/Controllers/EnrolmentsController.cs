
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrolmentsController : ControllerBase
{
    private readonly RaceDayDbContext _context;

    public EnrolmentsController(RaceDayDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "Participant")]
    public async Task<IActionResult> CreateEnrolment(
        CreateEnrolmentRequest request)
    {
        var userIdValue = HttpContext.User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out var userId))
        {
            return Unauthorized();
        }

        var participant = await _context.Participants
            .FirstOrDefaultAsync(p => p.UserID == userId);

        if (participant == null)
        {
            return Forbid();
        }

        var eventItem = await _context.Events
            .FirstOrDefaultAsync(e => e.EventID == request.EventID);

        if (eventItem == null)
        {
            return NotFound(new
            {
                message = "Event not found."
            });
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(c =>
                c.CategoryID == request.CategoryID);

        if (category == null || category.EventID != request.EventID)
        {
            return BadRequest(new
            {
                message = "The selected category does not belong to this event."
            });
        }

        var alreadyEnrolled = await _context.EventEnrolments
            .AnyAsync(e =>
                e.ParticipantID == participant.ParticipantID &&
                e.EventID == request.EventID);

        if (alreadyEnrolled)
        {
            return Conflict(new
            {
                message = "You are already enrolled in this event."
            });
        }

        var enrolment = new EventEnrolment
        {
            ParticipantID = participant.ParticipantID,
            EventID = request.EventID,
            CategoryID = request.CategoryID,
            EnrolmentDate = DateTime.UtcNow,
            Status = "Confirmed"
        };

        _context.EventEnrolments.Add(enrolment);
        await _context.SaveChangesAsync();

        return Created(
            $"/api/Enrolments/{enrolment.EnrolmentID}",
            new
            {
                enrolment.EnrolmentID,
                enrolment.ParticipantID,
                enrolment.EventID,
                enrolment.CategoryID,
                enrolment.EnrolmentDate,
                enrolment.Status
            });
    }

    [HttpGet("my")]
    [Authorize(Roles = "Participant")]
    public async Task<IActionResult> GetMyEnrolments()
    {
        var userIdValue = HttpContext.User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out var userId))
        {
            return Unauthorized();
        }

        var participant = await _context.Participants
            .FirstOrDefaultAsync(p => p.UserID == userId);

        if (participant == null)
        {
            return Forbid();
        }

        var enrolments = await _context.EventEnrolments
            .AsNoTracking()
            .Where(e => e.ParticipantID == participant.ParticipantID)
            .OrderByDescending(e => e.EnrolmentDate)
            .Select(e => new
            {
                e.EnrolmentID,
                e.EventID,
                EventName = e.Event.EventName,
                e.CategoryID,
                CategoryName = e.Category.CategoryName,
                e.EnrolmentDate,
                e.Status
            })
            .ToListAsync();

        return Ok(enrolments);
    }

    [HttpGet("event/{eventId:int}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> GetEventEnrolments(int eventId)
    {
        var userIdValue = HttpContext.User.FindFirstValue(
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

        var enrolments = await _context.EventEnrolments
            .AsNoTracking()
            .Where(e => e.EventID == eventId)
            .OrderBy(e => e.EnrolmentDate)
            .Select(e => new
            {
                e.EnrolmentID,
                e.ParticipantID,
                ParticipantName =
                    e.Participant.User.FirstName + " " +
                    e.Participant.User.LastName,
                e.CategoryID,
                CategoryName = e.Category.CategoryName,
                e.EnrolmentDate,
                e.Status
            })
            .ToListAsync();

        return Ok(enrolments);
    }
}
