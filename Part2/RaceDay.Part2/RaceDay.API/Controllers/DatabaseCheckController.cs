using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;

namespace RaceDay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DatabaseCheckController : ControllerBase
{
    private readonly RaceDayDbContext _context;

    public DatabaseCheckController(RaceDayDbContext context)
    {
        _context = context;
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _context.Roles
            .AsNoTracking()
            .Select(role => new
            {
                role.RoleID,
                role.RoleName
            })
            .ToListAsync();

        return Ok(roles);
    }
}