
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs;

public class UpdateProfileRequest
{
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Phone { get; set; }

    // Participant-specific fields
    public DateOnly? DateOfBirth { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    [StringLength(150)]
    public string? Club { get; set; }

    // Organiser-specific fields
    [StringLength(150)]
    public string? OrganisationName { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(150)]
    public string? Website { get; set; }
}
