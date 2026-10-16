using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RaceDayDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthController(
        RaceDayDbContext context,
        IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var emailExists = await _context.Users
            .AnyAsync(user => user.Email.ToLower() == email);

        if (emailExists)
        {
            return Conflict(new
            {
                message = "An account with this email already exists."
            });
        }

        var role = await _context.Roles
            .FirstOrDefaultAsync(role =>
                role.RoleName == request.Role);

        if (role == null)
        {
            return BadRequest(new
            {
                message = "The selected role does not exist."
            });
        }

        if (role.RoleName == "Organizer" &&
            string.IsNullOrWhiteSpace(request.OrganisationName))
        {
            return BadRequest(new
            {
                message = "Organisation name is required for an organiser."
            });
        }

        if (role.RoleName == "Participant" &&
            request.DateOfBirth == null)
        {
            return BadRequest(new
            {
                message = "Date of birth is required for a participant."
            });
        }

        var user = new User
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = email,
            RoleID = role.RoleID,
            Phone = request.Phone,
            DateCreated = DateTime.UtcNow,
            IsActive = true
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user, request.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        if (role.RoleName == "Organizer")
        {
            _context.Organizers.Add(new Organizer
            {
                UserID = user.UserID,
                OrganisationName = request.OrganisationName!.Trim(),
                Description = request.Description,
                Website = request.Website
            });
        }
        else
        {
            _context.Participants.Add(new Participant
            {
                UserID = user.UserID,
                DateOfBirth = request.DateOfBirth!.Value,
                Gender = request.Gender,
                Club = request.Club
            });
        }

        await _context.SaveChangesAsync();

        return Created("/api/Auth/login", new
        {
            user.UserID,
            user.FirstName,
            user.LastName,
            user.Email,
            Role = role.RoleName
        });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await _context.Users
            .Include(user => user.Role)
            .FirstOrDefaultAsync(user => user.Email == email);

        if (user == null || !user.IsActive)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        HttpContext.Session.SetInt32("UserID", user.UserID);
        HttpContext.Session.SetString("Role", user.Role.RoleName);

        return Ok(new
        {
            user.UserID,
            user.FirstName,
            user.LastName,
            user.Email,
            Role = user.Role.RoleName,
            Message = "Login successful."
        });
    }


    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var userId = HttpContext.Session.GetInt32("UserID");
        var role = HttpContext.Session.GetString("Role");

        if (userId == null || string.IsNullOrEmpty(role))
        {
            return Unauthorized(new
            {
                message = "You are not logged in."
            });
        }

        return Ok(new
        {
            UserID = userId,
            Role = role
        });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return Ok(new
        {
            message = "Logout successful."
        });
    }
}