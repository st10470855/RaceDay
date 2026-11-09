namespace MVC.ViewModels;

public class OrganizerEnrolmentViewModel
{
    public int EnrolmentID { get; set; }

    public int ParticipantID { get; set; }

    public string? ParticipantName { get; set; }

    public int CategoryID { get; set; }

    public string? CategoryName { get; set; }

    public DateTime EnrolmentDate { get; set; }

    public string? Status { get; set; }
}