
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs;

public class CreateEnrolmentRequest
{
    [Required]
    public int EventID { get; set; }

    [Required]
    public int CategoryID { get; set; }
}
