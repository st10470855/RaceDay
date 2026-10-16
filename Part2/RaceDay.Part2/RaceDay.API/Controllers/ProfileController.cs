
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;

namespace RaceDay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly RaceDayDbContext _context;

    public ProfileController(RaceDayDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var userIdValue = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out var userId))
        {
            return Unauthorized(new
            {
                message = "Unable to identify the logged-in user."
            });
        }

        var user = await _context.Users
            .AsNoTracking()
            .Where(u => u.UserID == userId)
            .Select(u => new
            {
                u.UserID,
                u.FirstName,
                u.LastName,
                u.Email,
                u.Phone,
                u.DateCreated,
                u.IsActive,
                Role = u.Role.RoleName
            })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound(new
            {
                message = "Profile not found."
            });
        }

        return Ok(user);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateProfile(
        RaceDay.API.DTOs.UpdateProfileRequest request)
    {
        var userIdValue = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out var userId))
        {
            return Unauthorized();
        }

        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserID == userId);

        if (user == null)
        {
            return NotFound(new
            {
                message = "Profile not found."
            });
        }

        var email = request.Email.Trim().ToLowerInvariant();

        var emailExists = await _context.Users.AnyAsync(
            u => u.Email == email && u.UserID != userId);

        if (emailExists)
        {
            return Conflict(new
            {
                message = "That email address is already in use."
            });
        }

        user.FirstName = request.FirstName.Trim();
        user.LastName = request.LastName.Trim();
        user.Email = email;
        user.Phone = request.Phone;

        if (user.Role.RoleName == "Participant")
        {
            var participant = await _context.Participants
                .FirstOrDefaultAsync(p => p.UserID == userId);

            if (participant == null)
            {
                return NotFound(new
                {
                    message = "Participant profile not found."
                });
            }

            if (request.DateOfBirth == null)
            {
                return BadRequest(new
                {
                    message = "Date of birth is required."
                });
            }

            participant.DateOfBirth = request.DateOfBirth.Value;
            participant.Gender = request.Gender;
            participant.Club = request.Club;
        }
        else if (user.Role.RoleName == "Organizer")
        {
            var organizer = await _context.Organizers
                .FirstOrDefaultAsync(o => o.UserID == userId);

            if (organizer == null)
            {
                return NotFound(new
                {
                    message = "Organiser profile not found."
                });
            }

            if (string.IsNullOrWhiteSpace(
                request.OrganisationName))
            {
                return BadRequest(new
                {
                    message = "Organisation name is required."
                });
            }

            organizer.OrganisationName =
                request.OrganisationName.Trim();
            organizer.Description = request.Description;
            organizer.Website = request.Website;
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Profile updated successfully.",
            user.UserID,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Phone,
            Role = user.Role.RoleName
        });
    }



















}
