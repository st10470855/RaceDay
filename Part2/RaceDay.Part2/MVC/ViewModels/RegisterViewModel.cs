
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8,
        ErrorMessage = "Password must be at least 8 characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select an account type.")]
    public string Role { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Phone { get; set; }

    // Organizer details
    [StringLength(150)]
    public string? OrganisationName { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(150)]
    public string? Website { get; set; }

    // Participant details
    [DataType(DataType.Date)]
    public DateOnly? DateOfBirth { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    [StringLength(150)]
    public string? Club { get; set; }
}
