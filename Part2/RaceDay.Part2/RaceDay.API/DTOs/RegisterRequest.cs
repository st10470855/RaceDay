using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs;

public class RegisterRequest
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

    [Required]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^(Organizer|Participant)$",
        ErrorMessage = "Role must be Organizer or Participant.")]
    public string Role { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Phone { get; set; }

    // Organizer-specific fields
    [StringLength(150)]
    public string? OrganisationName { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(150)]
    public string? Website { get; set; }

    // Participant-specific fields
    public DateOnly? DateOfBirth { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    [StringLength(150)]
    public string? Club { get; set; }
}