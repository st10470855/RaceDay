
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC.ViewModels;

public class CreateOrganizerEventViewModel
{
    [Required(ErrorMessage = "Event name is required.")]
    [StringLength(150)]
    public string EventName { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Please select an event date.")]
    [DataType(DataType.Date)]
    public DateOnly? EventDate { get; set; }

    [Required(ErrorMessage = "Event location is required.")]
    [StringLength(200)]
    public string Location { get; set; } = string.Empty;

    [Range(0.1, 500,
        ErrorMessage = "Distance must be between 0.1 and 500 km.")]
    public decimal Distance { get; set; }

    [Required(ErrorMessage = "Please select an event type.")]
    public string EventType { get; set; } = string.Empty;

    // Optional event banner image.
    public IFormFile? BannerImage { get; set; }
}
