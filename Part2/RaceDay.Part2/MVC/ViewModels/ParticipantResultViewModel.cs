
namespace MVC.ViewModels;

public class ParticipantResultViewModel
{
    public int ResultID { get; set; }

    public int EnrolmentID { get; set; }

    public string EventName { get; set; } = string.Empty;

    public TimeSpan FinishTime { get; set; }

    public int? FinishPosition { get; set; }

    public int? OverallPosition { get; set; }
}
