
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels;

public class ParticipantProfileViewModel
{
    public int UserID { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Enter a valid phone number.")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Date of birth is required.")]
    [DataType(DataType.Date)]
    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    [StringLength(150)]
    public string? Club { get; set; }

    public string Role { get; set; } = string.Empty;
}
