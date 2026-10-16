namespace RaceDay.API.Models;

public class Result
{
    public int ResultID { get; set; }

    public int EnrolmentID { get; set; }

    public TimeSpan FinishTime { get; set; }

    public int FinishPosition { get; set; }

    public int? OverallPosition { get; set; }

    public int? RecordedBy { get; set; }

    public DateTime RecordedAt { get; set; }

    public EventEnrolment EventEnrolment { get; set; } = null!;

    public User? RecordedByUser { get; set; }
}