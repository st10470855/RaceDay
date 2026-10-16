
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs;

public class UpdateEventRequest
{
    [Required]
    [StringLength(200)]
    public string EventName { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    public DateOnly? EventDate { get; set; }

    [Required]
    [StringLength(200)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [Range(typeof(decimal), "0.01", "9999.99")]
    public decimal Distance { get; set; }

    [Required]
    [RegularExpression(
        "^(Run|Walk|Cycle)$",
        ErrorMessage = "Event type must be Run, Walk, or Cycle.")]
    public string EventType { get; set; } = string.Empty;
}
