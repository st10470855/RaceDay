
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
public class ResultsController : ControllerBase
{
    private readonly RaceDayDbContext _context;

    public ResultsController(RaceDayDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> RecordResult(
        RecordResultRequest request)
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

        var enrolment = await _context.EventEnrolments
            .Include(e => e.Event)
            .FirstOrDefaultAsync(
                e => e.EnrolmentID == request.EnrolmentID);

        if (enrolment == null)
        {
            return NotFound(new
            {
                message = "Enrolment not found."
            });
        }

        if (enrolment.Event.OrganizerID != organizer.OrganizerID)
        {
            return Forbid();
        }

        if (request.FinishTime < TimeSpan.Zero ||
            request.FinishTime >= TimeSpan.FromDays(1))
        {
            return BadRequest(new
            {
                message = "Finish time must be between 00:00:00 and 23:59:59."
            });
        }

        var resultExists = await _context.Results
            .AnyAsync(r => r.EnrolmentID == request.EnrolmentID);

        if (resultExists)
        {
            return Conflict(new
            {
                message = "A result already exists for this enrolment."
            });
        }

        var result = new RaceDay.API.Models.Result
        {
            EnrolmentID = request.EnrolmentID,
            FinishTime = request.FinishTime,
            FinishPosition = request.FinishPosition,
            OverallPosition = request.OverallPosition,
            RecordedBy = userId,
            RecordedAt = DateTime.UtcNow
        };

        _context.Results.Add(result);
        await _context.SaveChangesAsync();

        return Created(
            $"/api/Results/{result.ResultID}",
            new
            {
                result.ResultID,
                result.EnrolmentID,
                result.FinishTime,
                result.FinishPosition,
                result.OverallPosition,
                result.RecordedAt
            });
    }

    [HttpGet("my")]
    [Authorize(Roles = "Participant")]
    public async Task<IActionResult> GetMyResults()
    {
        var userIdValue = User.FindFirstValue(
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

        var results = await _context.Results
            .AsNoTracking()
            .Where(r =>
                r.EventEnrolment.ParticipantID ==
                participant.ParticipantID)
            .OrderBy(r => r.FinishPosition)
            .Select(r => new
            {
                r.ResultID,
                r.EnrolmentID,
                EventName = r.EventEnrolment.Event.EventName,
                r.FinishTime,
                r.FinishPosition,
                r.OverallPosition
            })
            .ToListAsync();

        return Ok(results);
    }
}
