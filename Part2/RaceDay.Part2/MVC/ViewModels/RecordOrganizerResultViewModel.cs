using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels;

public class RecordOrganizerResultViewModel
{
    public int EnrolmentID { get; set; }

    public int EventID { get; set; }

    public string? ParticipantName { get; set; }

    [Required(ErrorMessage = "Finish time is required.")]
    public TimeSpan FinishTime { get; set; }

    [Range(1, 100000)]
    public int FinishPosition { get; set; }

    [Range(1, 100000)]
    public int OverallPosition { get; set; }
}