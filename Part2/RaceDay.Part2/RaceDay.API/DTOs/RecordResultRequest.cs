
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs;

public class RecordResultRequest
{
    [Required]
    public int EnrolmentID { get; set; }

    [Required]
    public TimeSpan FinishTime { get; set; }

    [Range(1, int.MaxValue)]
    public int FinishPosition { get; set; }

    [Range(1, int.MaxValue)]
    public int? OverallPosition { get; set; }
}
