
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RaceDay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationTestController : ControllerBase
{
    [HttpGet("organizer")]
    [Authorize(Roles = "Organizer")]
    public IActionResult OrganizerOnly()
    {
        return Ok(new
        {
            message = "Access granted to Organiser.",
            role = "Organizer"
        });
    }

    [HttpGet("participant")]
    [Authorize(Roles = "Participant")]
    public IActionResult ParticipantOnly()
    {
        return Ok(new
        {
            message = "Access granted to Participant.",
            role = "Participant"
        });
    }
}
